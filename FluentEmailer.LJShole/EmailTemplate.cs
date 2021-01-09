using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentEmailer.LJShole
{
    public class EmailTemplate : IEmailTemplate
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

        /// <summary>
        /// The body of the email will use this template file. 
        /// </summary>
        /// <param name="templateLocation">Location of the template file to use as the body of the email. NB This does not support URI's.</param>
        /// <returns></returns>
        public IEmailTemplate UsingEmailTemplate(string templateLocation)
        {
            if (string.IsNullOrEmpty(templateLocation))
                throw new ArgumentNullException(nameof(templateLocation));

            _templateLocation = templateLocation;
            return this;
        }

        /// <summary>
        /// Dictionary containing key/value pairs. Keys in this dictionary represent placeholders found on the template file specified / to be specified.
        /// </summary>
        /// <param name="templateValues"></param>
        /// <returns></returns>
        public IEmailTemplate UsingTemplateDictionary(Dictionary<string, string> templateValues)
        {
            if (templateValues == null || templateValues.Keys.Count == 0)
                throw new InvalidOperationException("Template values are required.");

            _templateValues = templateValues;
            return this;
        }
        /// <summary>
        /// Sets the body of the email to a this string.
        /// </summary>
        /// <param name="emailBody"></param>
        /// <returns></returns>
        public IEmailMessage UsingString(string emailBody)
        {
            if (string.IsNullOrEmpty(emailBody))
                throw new ArgumentNullException(nameof(emailBody));

            _emailBody = emailBody;

            _emailMessage.SetBody(_emailBody);

            return _emailMessage;
        }
        /// <summary>
        /// Replaces the placeholders in the template file with the dictionary specified in the 'UsingTemplateDictionary' method.
        /// </summary>
        /// <returns></returns>
        public IEmailMessage CompileTemplate()
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
