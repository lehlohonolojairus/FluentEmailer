using FluentEmailer.LJShole.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace FluentEmailer.LJShole
{
    /// <summary>
    /// Mail message body set up class.
    /// </summary>
    public class EmailBody : IEmailBody
    {
        private IEmailTemplate _emailTemplate;
        private readonly IEmailMessage _emailMessage;
        private readonly IMailer _mailer;
        private bool _emailBodyIsHtml;
        private readonly IEmailBody _emailBodyInstance;

        public bool EmailBodyIsHtml { get { return _emailBodyIsHtml; } }

        public EmailBody(IEmailMessage emailMessage, IMailer mailer)
        {
            _emailMessage = emailMessage;
            _mailer = mailer;
        }

        /// <summary>
        /// Bootstrap setting up the body of the email using a template file.
        /// </summary>
        /// <returns></returns>
        public IEmailTemplate Body()
        {
            _emailTemplate = _emailTemplate ?? new EmailTemplate(_emailMessage, _mailer, _emailBodyInstance);
            return _emailTemplate;
        }

        /// <summary>
        /// Set the Encoding for the body of the mail.
        /// </summary>
        /// <param name="encoding">Encoding value</param>
        /// <returns></returns>
        public IEmailBody SetBodyEncoding(Encoding encoding)
        {
            (_emailMessage as EmailMessage).SetBodyEncoding(encoding);
            return this;
        }

        /// <summary>
        /// Set the TranserEncoding for the body of the mail.
        /// </summary>
        /// <param name="transferEncoding">TransferEncoding value</param>
        /// <returns></returns>
        public IEmailBody SetBodyTransferEncoding(TransferEncoding transferEncoding)
        {
            (_emailMessage as EmailMessage).SetBodyTransferEncoding(transferEncoding);
            return this;
        }
      
        /// <summary>
        /// Indicate if the body of the email is HTML.
        /// </summary>
        /// <param name="emailBodyIsHtml">Boolean value indicating if the body of the email is HTML.</param>
        /// <returns></returns>
        public IEmailMessage SetBodyIsHtmlFlag(bool emailBodyIsHtml = true)
        {
            _emailBodyIsHtml = emailBodyIsHtml;
            (_emailMessage as EmailMessage).SetMessageInstance(this);
            return _emailMessage;
        }
    }
}
