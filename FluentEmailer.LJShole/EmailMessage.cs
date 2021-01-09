using FluentEmailer.LJShole.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace FluentEmailer.LJShole
{
    public class EmailMessage : IEmailMessage
    {
        private EmailTemplate Template;
        private Mailer Mailer;
        private IMailCredentials MailCredentials;
        private string Subject;
        private string Body;
        private bool EmailBodyIsHtml;
        private List<MailAddress> ToMailAddresses = new List<MailAddress>();
        private List<MailAddress> CcMailAddresses = new List<MailAddress>();
        private List<MailAddress> BccMailAddresses = new List<MailAddress>();
        private MailAddress FromMailAddress;
        private List<Attachment> Attachments;

        public EmailMessage(Mailer mailer)
        {
            Mailer = mailer;
        }
        /// <summary>
        /// Bootstrapper for setting up the body of the email to be sent.
        /// </summary>
        /// <returns></returns>
        public IEmailTemplate WithBody()
        {
            Template = Template ?? new EmailTemplate(this, Mailer);
            return Template;
        }

        /// <summary>
        /// Set up SMTP /IMAP server credentials.
        /// </summary>
        /// <returns></returns>
        public IMailCredentials WithCredentials()
        {
            MailCredentials = MailCredentials ?? new MailCredentials(Mailer);
            return MailCredentials;
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

            Subject = subject;
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

            ToMailAddresses = toMailAddresses;
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
            CcMailAddresses = ccMailAddresses;
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
            BccMailAddresses = bccMailAddresses;
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

            FromMailAddress = fromMailAddress;
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
            Attachments = attachments;
            return this;
        }

        public IEmailMessage SetBodyIsHtmlFlag(bool emailBodyIsHtml = true)
        {
            EmailBodyIsHtml = emailBodyIsHtml;
            return this;
        }

        internal IEmailMessage SetBody(string emailBody)
        {
            Body = emailBody;
            return this;
        }

        public MailMessage GetMessage()
        {
            var message = new MailMessage
            {
                Subject = Subject,
                From = FromMailAddress
            };

            ToMailAddresses.ForEach(address => { message.To.Add(address); });
            CcMailAddresses?.ForEach(address => { message.To.Add(address); });
            BccMailAddresses?.ForEach(address => { message.To.Add(address); });
            Attachments?.ForEach(attachment => { message.Attachments.Add(attachment); });
            message.IsBodyHtml = EmailBodyIsHtml;
            message.Priority = MailPriority.High;
            message.Body = Body;

            return message;
        }
    }
}
