using FluentEmailer.LJShole.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace FluentEmailer.LJShole.EmailChannel.SMTP
{
    /// <summary>
    /// Mail message to be sent.
    /// </summary>
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
        private MailAddress _senderMailAddress;
        private IEnumerable<Attachment> _attachments;
        private TransferEncoding _transferEncoding = TransferEncoding.Unknown;
        private MailAddressCollection _replyToEmails;
        private IEnumerable<MailAddress> _toMailAddresses = new List<MailAddress>();
        private IEnumerable<MailAddress> _ccMailAddresses = new List<MailAddress>();
        private IEnumerable<MailAddress> _bccMailAddresses = new List<MailAddress>();
        private IEmailSender _emailSender;

        /// <summary>
        /// To emal addresses list 
        /// </summary>
        public IEnumerable<MailAddress> ToMailAddressesList { get { return _toMailAddresses; } }

        /// <summary>
        /// CC email addresses list
        /// </summary>
        public IEnumerable<MailAddress> CcMailAddressesList { get { return _ccMailAddresses; } }

        /// <summary>
        /// BCC list email addresses.
        /// </summary>
        public IEnumerable<MailAddress> BccMailAddressesList { get { return _bccMailAddresses; } }

        internal EmailMessage(string subject, string hostServer, string portNumber, string userName, string password, bool sslRequired, MailAddress senderMailAddress, IEnumerable<MailAddress> toEmailAddresses)
        {
            _hostServer = hostServer;
            _portNumber = portNumber;
            _userName = userName;
            _password = password;
            _sslRequired = sslRequired;
            _toMailAddresses = toEmailAddresses;
            _subject = subject;
            _senderMailAddress = senderMailAddress;
            _fromMailAddress = senderMailAddress;
        }
        /// <summary>
        /// Set the encoding of the body. If you are using UTF8, no need to set this value.
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public IEmailMessage BodyEncoding(Encoding encoding)
        {
            _bodyEncoding = encoding;
            return this;
        }

        /// <summary>
        /// Set the body transfer encoding. If you are using Unknown, no need to set this value.
        /// </summary>
        /// <param name="transferEncoding"></param>
        /// <returns></returns>
        public IEmailMessage BodyTransferEncoding(TransferEncoding transferEncoding)
        {
            _transferEncoding = transferEncoding;
            return this;
        }

        /// <summary>
        /// Set the encoding type of the subject. Default is UTF8
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
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
        public IEmailMessage FromMailAddress(MailAddress fromMailAddress)
        {
            if (fromMailAddress == null || string.IsNullOrEmpty(fromMailAddress.Address))
                throw new ArgumentNullException(nameof(fromMailAddress));

            _fromMailAddress = fromMailAddress;
            return this;
        }
        /// <summary>
        /// Add a sender email address.
        /// </summary>
        /// <param name="senderMailAddress">Sender email address.</param>
        /// <returns></returns>
        public IEmailMessage SenderMailAddress(MailAddress senderMailAddress)
        {
            if (senderMailAddress == null || string.IsNullOrEmpty(senderMailAddress.Address))
                throw new ArgumentNullException(nameof(senderMailAddress));

            _senderMailAddress = senderMailAddress;
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

        /// <summary>
        /// Set the priority level of the email.
        /// </summary>
        /// <param name="mailPriority"></param>
        /// <returns></returns>
        public IEmailMessage SetPriority(MailPriority mailPriority)
        {
            _priority = mailPriority;

            return this;
        }

        /// <summary>
        /// Adds reply-to email address for the email.
        /// </summary>
        /// <param name="replyToEmail">The reply-to email address </param>
        /// <returns></returns>
        public IEmailMessage ReplyTo(MailAddress replyToEmail)
        {
            _replyToEmail = replyToEmail;

            return this;
        }
        /// <summary>
        /// Adds reply-to email addresses for the email.
        /// </summary>
        /// <param name="replyToEmails">List of email addresses.</param>
        /// <returns></returns>
        public IEmailMessage ReplyTo(MailAddressCollection replyToEmails)
        {
            _replyToEmails = replyToEmails;

            return this;
        }

        /// <summary>
        /// Sets the body of the email message.
        /// </summary>
        /// <param name="messageBody">Message body of the email.</param>
        /// <param name="bodyIsHTML">Flag to set the body as HTML.</param>
        /// <returns></returns>
        public IEmailSender Body(string messageBody, bool bodyIsHTML = true)
        {
            _bodyIsHTML = bodyIsHTML;

            _body = messageBody;

            var message = CreateMailMessageInstance();

            _emailSender = new EmailSender(message, _hostServer, _portNumber, _userName, _password, _sslRequired);

            return _emailSender;
        }

        /// <summary>
        /// Sets the body of the email message. This overload uses a template file (.html or txt) along with key-value dictionary that should be used to substitute placeholders in the template.
        /// </summary>
        /// <param name="templateValues">A dictionary containing keys found in the template with values that should be replaced in the template.</param>
        /// <param name="TemplateFileLocation">File location of the template. Ensure this file is accessible by the application.</param>
        /// <param name="bodyIsHTML">Flag to set the body as HTML.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public IEmailSender Body(IDictionary<string, string> templateValues, string TemplateFileLocation, bool bodyIsHTML = true)
        {
            _bodyIsHTML = bodyIsHTML;

            if (!File.Exists(TemplateFileLocation))
                throw new FileNotFoundException(TemplateFileLocation);

            _body = ReadTemplateFile(TemplateFileLocation, templateValues);

            var message = CreateMailMessageInstance();

            _emailSender = new EmailSender(message, _hostServer, _portNumber, _userName, _password, _sslRequired);

            return _emailSender;
        }

        private string ReadTemplateFile(string templateFileLocation, IDictionary<string, string> templateValues)
        {
            try
            {
                using var reader = new StreamReader(templateFileLocation);

                var templateContents = new StringBuilder();

                templateContents.Append(reader.ReadToEnd());

                foreach (var templateValue in templateValues)
                    templateContents.Replace(templateValue.Key, templateValue.Value);

                return templateContents.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private MailMessage CreateMailMessageInstance()
        {
            if (string.IsNullOrWhiteSpace(_body)) throw new ArgumentNullException(nameof(_body));

            if (_fromMailAddress == null) throw new InvalidOperationException("From MailAddress is not set");

            if (_toMailAddresses is null || _toMailAddresses.Count() == 0) throw new InvalidOperationException("Please set MailAddress(es) to send email to");


            var message = new MailMessage
            {
                From = _fromMailAddress,
                Body = _body,
                Subject = _subject,
                IsBodyHtml = _bodyIsHTML,
                Priority = _priority,
                BodyEncoding = _bodyEncoding,
                SubjectEncoding = _subjectEncoding,
                BodyTransferEncoding = _transferEncoding,
                Sender = _senderMailAddress,
            };

            _toMailAddresses?.ToList().ForEach(address => { message.To.Add(address); });
            _ccMailAddresses?.ToList().ForEach(address => { message.CC.Add(address); });
            _bccMailAddresses?.ToList().ForEach(address => { message.Bcc.Add(address); });
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