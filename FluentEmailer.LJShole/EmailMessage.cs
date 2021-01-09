using System.Collections.Generic;
using System.Net.Mail;

namespace FluentEmailer.LJShole
{
    public class EmailMessage
    {
        public EmailTemplate _template;
        public Mailer _mailer;
        public MailCredentials _mailCredentials;
        public string _subject;
        public string _body;
        public bool _emailBodyIsHtml;
        public List<MailAddress> _toMailAddresses = new List<MailAddress>();
        public List<MailAddress> _ccMailAddresses = new List<MailAddress>();
        public List<MailAddress> _bccMailAddresses = new List<MailAddress>();
        public MailAddress _fromMailAddress;

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
            _subject = subject;
            return this;
        }
        public EmailMessage AddToMailAddresses(List<MailAddress> mailAddresses)
        {
            _toMailAddresses = mailAddresses;
            return this;
        }
        public EmailMessage AddCcMailAddresses(List<MailAddress> mailAddresses)
        {
            _ccMailAddresses = mailAddresses;
            return this;
        }
        public EmailMessage AddBccMailAddresses(List<MailAddress> mailAddresses)
        {
            _bccMailAddresses = mailAddresses;
            return this;
        }
        public EmailMessage AddFromMailAddresses(MailAddress mailAddress)
        {
            _fromMailAddress = mailAddress;
            return this;
        }
        public EmailMessage SetBodyIsHtmlFlag(bool emailBodyIsHtml = true)
        {
            _emailBodyIsHtml = emailBodyIsHtml;
            return this;
        }

        public EmailMessage SetBody(string emailBody)
        {
            _body = emailBody;
            return this;
        }
    }
}
