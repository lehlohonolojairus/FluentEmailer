using FluentEmailer.LJShole.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace FluentEmailer.LJShole
{
    public class EmailMessage : IEmailMessage
    {
        private readonly string _hostServer;
        private readonly string _portNumber;
        private readonly string _userName;
        private readonly string _password;
        private readonly bool _sslRequired;
        private string _subject;
        private string _body;
        private bool _bodyIsHTML;
        private Encoding _bodyEncoding = Encoding.UTF8;
        private MailPriority _priority = MailPriority.Normal;
        private MailAddress _replyToEmail;
        private Encoding _subjectEncoding = Encoding.UTF8;
        private MailAddress _fromMailAddress;
        private IEnumerable<Attachment> _attachments;
        private TransferEncoding _transferEncoding = TransferEncoding.Unknown;
        private MailAddressCollection _replyToEmails;
        private IEnumerable<MailAddress> _toMailAddresses = new List<MailAddress>();
        private IEnumerable<MailAddress> _ccMailAddresses = new List<MailAddress>();
        private IEnumerable<MailAddress> _bccMailAddresses = new List<MailAddress>();
        private IEmailSender _emailSender;
        internal EmailMessage(string hostServer, string portNumber, string userName, string password, bool sslRequired, IEnumerable<MailAddress> toEmailAddresses)
        {
            _hostServer = hostServer;
            _portNumber = portNumber;
            _userName = userName;
            _password = password;
            _sslRequired = sslRequired;
            _toMailAddresses = toEmailAddresses;
        }
        public IEmailMessage SetBodyEncoding(Encoding encoding)
        {
            _bodyEncoding = encoding;
            return this;
        }

        public IEmailMessage SetBodyTransferEncoding(TransferEncoding transferEncoding)
        {
            _transferEncoding = transferEncoding;
            return this;
        }
        public IEmailMessage SubjectEncoding(Encoding encoding)
        {
            _subjectEncoding = encoding;
            return this;
        }
        /// <summary>
        /// Sets the subject of the email.
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        public IEmailMessage Subject(string subject)
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
        public IEmailMessage ToMailAddresses(IEnumerable<MailAddress> toMailAddresses)
        {
            if (toMailAddresses == null || toMailAddresses.Count() == 0)
                throw new ArgumentNullException(nameof(toMailAddresses));

            _toMailAddresses = toMailAddresses;
            return this;
        }

        /// <summary>
        /// Add a list of CC email addresses the email to be sent to.
        /// </summary>
        /// <param name="ccMailAddresses">List of CC email addresses to receive the email.</param>
        /// <returns></returns>
        public IEmailMessage CcMailAddresses(IEnumerable<MailAddress> ccMailAddresses)
        {
            if (ccMailAddresses == null || ccMailAddresses.Count() == 0)
                throw new ArgumentNullException(nameof(ccMailAddresses));
            _ccMailAddresses = ccMailAddresses;
            return this;
        }

        /// <summary>
        /// Add a list of BCC email addresses the email to be sent to.
        /// </summary>
        /// <param name="bccMailAddresses">List of BCC email addresses to receive the email.</param>
        /// <returns></returns>
        public IEmailMessage BccMailAddresses(IEnumerable<MailAddress> bccMailAddresses)
        {
            if (bccMailAddresses == null || bccMailAddresses.Count() == 0)
                throw new ArgumentNullException(nameof(bccMailAddresses));
            _bccMailAddresses = bccMailAddresses;
            return this;
        }

        /// <summary>
        /// Add a from email address.
        /// </summary>
        /// <param name="fromMailAddress">From email address for recepients to know where the email originated from.</param>
        /// <returns></returns>
        public IEmailMessage FromMailAddresses(MailAddress fromMailAddress)
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
        public IEmailMessage AddAttachments(IEnumerable<Attachment> attachments)
        {
            if (attachments == null || attachments.Count() == 0)
                throw new ArgumentNullException(nameof(attachments));
            _attachments = attachments;
            return this;
        }

        public IEmailMessage SetPriority(MailPriority mailPriority)
        {
            _priority = mailPriority;
            return this;
        }

        public IEmailMessage ReplyTo(MailAddress replyToEmail)
        {
            _replyToEmail = replyToEmail;
            return this;
        }
        public IEmailMessage ReplyTo(MailAddressCollection replyToEmails)
        {
            _replyToEmails = replyToEmails;
            return this;
        }

        public IEmailSender Body(string messageBody, bool bodyIsHTML = true)
        {
            _bodyIsHTML = bodyIsHTML;

            _body = messageBody;

            var message = CreateMailMessageInstance();

            _emailSender = new EmailSender(message, _hostServer, _portNumber, _userName, _password, _sslRequired);

            return _emailSender;
        }

        public IEmailSender Body(IDictionary<string, string> templateValues,string TemplateFileLocation, bool bodyIsHTML = true)
        {
            throw new NotImplementedException();
        }

        private MailMessage CreateMailMessageInstance()
        {
            if (string.IsNullOrWhiteSpace(_body)) throw new ArgumentNullException(nameof(_body));

            if (_fromMailAddress == null) throw new InvalidOperationException("From MailAddress is not set");

            if (_toMailAddresses is null || _toMailAddresses.Count() == 0) throw new InvalidOperationException("Please set MailAddress(es) to send email to");


            var message = new MailMessage();
            message.From = _fromMailAddress;
            message.Body = _body;
            message.IsBodyHtml = _bodyIsHTML;
            message.Priority = _priority;
            message.BodyEncoding = _bodyEncoding;
            message.BodyTransferEncoding = _transferEncoding;

            _toMailAddresses?.ToList().ForEach(address => { message.To.Add(address); });
            _ccMailAddresses ?.ToList().ForEach(address => { message.To.Add(address); });
            _bccMailAddresses?.ToList().ForEach(address => { message.To.Add(address); });
            _attachments?.ToList().ForEach(attachment => { message.Attachments.Add(attachment); });

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

  
    }
}