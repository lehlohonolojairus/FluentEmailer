using FluentEmailer.LJShole.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace FluentEmailer.LJShole
{
    public class EmailMessage : IEmailMessage
    {
        private EmailTemplate _template;
        private readonly Mailer _mailer;
        private IMailCredentials _mailCredentials;
        private string _subject;
        private string _body;
        private List<MailAddress> _toMailAddresses = new List<MailAddress>();
        private List<MailAddress> _ccMailAddresses = new List<MailAddress>();
        private List<MailAddress> _bccMailAddresses = new List<MailAddress>();
        private MailAddress _fromMailAddress;
        private List<Attachment> _attachments;
        private TransferEncoding _transferEncoding;
        private Encoding _bodyEncoding;
        private MailPriority _priority;
        private MailAddress _replyToEmail;
        private MailAddressCollection _replyToEmails;
        private Encoding _subjectEncoding;

        internal EmailBody EmailBodyInstance { get; set; }

        internal void SetBodyEncoding(Encoding encoding)
        {
            _bodyEncoding = encoding;
        }

        public EmailMessage(Mailer mailer)
        {
            _mailer = mailer;
        }

        internal void SetMessageInstance(EmailBody emailBody)
        {
            this.EmailBodyInstance = emailBody;
        }

        internal void SetBodyTransferEncoding(TransferEncoding transferEncoding)
        {
            _transferEncoding = transferEncoding;
        }

        /// <summary>
        /// Bootstrapper for setting up the body of the email to be sent.
        /// </summary>
        /// <returns></returns>
        public IEmailBody SetUpBody()
        {
            EmailBodyInstance = EmailBodyInstance ?? new EmailBody(this, _mailer);
            _template = _template ?? new EmailTemplate(this, _mailer, EmailBodyInstance);
            return EmailBodyInstance;
        }

        /// <summary>
        /// Set up SMTP /IMAP server credentials.
        /// </summary>
        /// <returns></returns>
        public IMailCredentials WithCredentials()
        {
            _mailCredentials = _mailCredentials ?? new MailCredentials(_mailer);
            return _mailCredentials;
        }

        /// <summary>
        /// Sets the subject of the email.
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        public IEmailMessage WithSubject(string subject)
        {
            if (string.IsNullOrEmpty(subject))
                throw new ArgumentNullException(nameof(subject));

            _subject = subject;
            return this;
        }

        /// <summary>
        /// Add a list of email addresses the email to be sent to.
        /// </summary>
        /// <param name="toMailAddresses">List of email addresses to receive the email.</param>
        /// <returns></returns>
        public IEmailMessage AddToMailAddresses(List<MailAddress> toMailAddresses)
        {
            if (toMailAddresses == null || toMailAddresses.Count == 0)
                throw new ArgumentNullException(nameof(toMailAddresses));

            _toMailAddresses = toMailAddresses;
            return this;
        }

        /// <summary>
        /// Add a list of CC email addresses the email to be sent to.
        /// </summary>
        /// <param name="ccMailAddresses">List of CC email addresses to receive the email.</param>
        /// <returns></returns>
        public IEmailMessage AddCcMailAddresses(List<MailAddress> ccMailAddresses)
        {
            if (ccMailAddresses == null || ccMailAddresses.Count == 0)
                throw new ArgumentNullException(nameof(ccMailAddresses));
            _ccMailAddresses = ccMailAddresses;
            return this;
        }

        /// <summary>
        /// Add a list of BCC email addresses the email to be sent to.
        /// </summary>
        /// <param name="bccMailAddresses">List of BCC email addresses to receive the email.</param>
        /// <returns></returns>
        public IEmailMessage AddBccMailAddresses(List<MailAddress> bccMailAddresses)
        {
            if (bccMailAddresses == null || bccMailAddresses.Count == 0)
                throw new ArgumentNullException(nameof(bccMailAddresses));
            _bccMailAddresses = bccMailAddresses;
            return this;
        }

        /// <summary>
        /// Add a from email address.
        /// </summary>
        /// <param name="fromMailAddress">From email address for recepients to know where the email originated from.</param>
        /// <returns></returns>
        public IEmailMessage AddFromMailAddresses(MailAddress fromMailAddress)
        {
            if (fromMailAddress == null || string.IsNullOrEmpty(fromMailAddress.Address))
                throw new ArgumentNullException(nameof(fromMailAddress));

            _fromMailAddress = fromMailAddress;
            return this;
        }

        /// <summary>
        /// Allows the adding of attachments on the email.
        /// </summary>
        /// <param name="attachments">File attachments to attach to the email message.</param>
        /// <returns></returns>
        public IEmailMessage WithAttachments(List<Attachment> attachments)
        {
            if (attachments == null || attachments.Count == 0)
                throw new ArgumentNullException(nameof(attachments));
            _attachments = attachments;
            return this;
        }

        internal IEmailMessage SetBody(string emailBody)
        {
            _body = emailBody;
            return this;
        }

        public MailMessage GetMessage()
        {
            var message = new MailMessage
            {
                Subject = _subject,
                From = _fromMailAddress
            };

            _toMailAddresses.ForEach(address => { message.To.Add(address); });
            _ccMailAddresses?.ForEach(address => { message.To.Add(address); });
            _bccMailAddresses?.ForEach(address => { message.To.Add(address); });
            _attachments?.ForEach(attachment => { message.Attachments.Add(attachment); });
            message.IsBodyHtml = EmailBodyInstance.EmailBodyIsHtml;
            message.Priority = _priority;
            message.Body = _body;
            message.BodyEncoding = _bodyEncoding;
            message.BodyTransferEncoding = _transferEncoding;
            if (_replyToEmails != null && _replyToEmails.Count > 0)
            {
                _replyToEmails.ToList().ForEach(mail =>
                {
                    if (!string.IsNullOrEmpty(mail.Address))
                    {
                        message.ReplyToList.Add(mail);
                    }
                });
            }
            if (_replyToEmail != null && !string.IsNullOrEmpty(_replyToEmail.Address))
            {
                message.ReplyToList.Add(_replyToEmail);
            }
            return message;
        }

        public IEmailMessage SetSubjectEncoding(Encoding encoding)
        {
            _subjectEncoding = encoding;
            return this;
        }

        public IEmailMessage SetPriority(MailPriority mailPriority)
        {
            _priority = mailPriority;
            return this;
        }

        public IEmailMessage WithReplyTo(MailAddress replyToEmail)
        {
            _replyToEmail = replyToEmail;
            return this;
        }
        public IEmailMessage WithReplyTo(MailAddressCollection replyToEmails)
        {
            _replyToEmails = replyToEmails;
            return this;
        }
    }
}
