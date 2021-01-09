using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentEmailer.LJShole
{
    public class EmailTemplate
    {
        public Mailer _mailer;
        public EmailMessage _emailMessage;
        public MailCredentials _mailCredentials;
        public string _emailBody;
        public string _templateLocation;
        public Dictionary<string, string> _templateValues;

        public EmailTemplate(EmailMessage emailMessage, Mailer mailer)
        {
            _emailMessage = emailMessage;
            _mailer = mailer;
        }

        public EmailTemplate UsingEmailTemplate(string templateLocation)
        {
            if (string.IsNullOrEmpty(templateLocation))
                throw new ArgumentNullException(nameof(templateLocation));

            _templateLocation = templateLocation;
            return this;
        }

        public EmailTemplate UsingTemplateDictionary(Dictionary<string, string> templateValues)
        {
            if (templateValues == null || templateValues.Keys.Count == 0)
                throw new InvalidOperationException("Template values are required.");

            _templateValues = templateValues;
            return this;
        }
        public EmailTemplate UsingString(string emailBody)
        {
            if (string.IsNullOrEmpty(emailBody))
                throw new ArgumentNullException(nameof(emailBody));

            _emailBody = emailBody;
            return this;
        }
        public EmailMessage BuildMessageBody()
        {
            if (string.IsNullOrEmpty(_emailBody))
            {
                using (var reader = new StreamReader(_templateLocation))
                {
                    _emailBody = reader.ReadToEnd();
                    _templateValues.Keys.ToList().ForEach(key =>
                    {
                        _emailBody = _emailBody.Replace(key, _templateValues[key]);
                    });
                }
            }

            _emailMessage.SetBody(_emailBody);

            return _emailMessage;
        }

    }
}
