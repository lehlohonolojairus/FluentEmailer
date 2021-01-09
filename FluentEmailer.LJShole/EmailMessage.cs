using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace FluentEmailer.LJShole
{
    public class EmailMessage
    {
        internal EmailTemplate _template;
        internal Mailer _mailer;
        internal MailCredentials _mailCredentials;
        internal string _subject;
        internal string _body;
        internal bool _emailBodyIsHtml;
        internal List<MailAddress> _toMailAddresses = new List<MailAddress>();
        internal List<MailAddress> _ccMailAddresses = new List<MailAddress>();
        internal List<MailAddress> _bccMailAddresses = new List<MailAddress>();
        internal MailAddress _fromMailAddress;
        internal List<Attachment> _attachments;

        public EmailMessage(Mailer mailer)
        {
            _mailer = mailer;
        }
        public EmailTemplate WithBody()
        {
            _template = _template ?? new EmailTemplate(this, _mailer);
            return _template;
        }
        public MailCredentials WithCredentials()
        {
            _mailCredentials = _mailCredentials ?? new MailCredentials(_mailer);
            return _mailCredentials;
        }
        public EmailMessage WithSubject(string subject)
        {
            if (string.IsNullOrEmpty(subject))
                throw new ArgumentNullException(nameof(subject));

            _subject = subject;
            return this;
        }
        public EmailMessage AddToMailAddresses(List<MailAddress> mailAddresses)
        {
            if (mailAddresses == null || mailAddresses.Count == 0)
                throw new ArgumentNullException(nameof(mailAddresses));

            _toMailAddresses = mailAddresses;
            return this;
        }
        public EmailMessage AddCcMailAddresses(List<MailAddress> mailAddresses)
        {
            if (mailAddresses == null || mailAddresses.Count == 0)
                throw new ArgumentNullException(nameof(mailAddresses));
            _ccMailAddresses = mailAddresses;
            return this;
        }
        public EmailMessage AddBccMailAddresses(List<MailAddress> mailAddresses)
        {
            if (mailAddresses == null || mailAddresses.Count == 0)
                throw new ArgumentNullException(nameof(mailAddresses));
            _bccMailAddresses = mailAddresses;
            return this;
        }
        public EmailMessage AddFromMailAddresses(MailAddress mailAddress)
        {
            _fromMailAddress = mailAddress;
            return this;
        }
        public EmailMessage WithAttachments(List<Attachment> attachments)
        {
            if (attachments == null || attachments.Count == 0)
                throw new ArgumentNullException(nameof(attachments));
            _attachments = attachments;
            return this;
        }

        public EmailMessage SetBodyIsHtmlFlag(bool emailBodyIsHtml = true)
        {
            _emailBodyIsHtml = emailBodyIsHtml;
            return this;
        }

        internal EmailMessage SetBody(string emailBody)
        {
            _body = emailBody;
            return this;
        }
    }
}
