using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace FluentEmailer.LJShole.EmailChannel.SMTP
{
    /// <summary>
    /// Send emails using SMTP/IMAP Server
    /// </summary>
    public class SMTPServer : ISMTPServer
    {
        private readonly string _hostServer;
        private readonly string _portNumber;
        private readonly string _userName;
        private readonly string _password;
        private readonly bool _sslRequired;

        private IEmailMessage _mailMessage;

        internal SMTPServer(string hostName, string portNumber, string userName, string password, bool sslRequired)
        {
            _hostServer = hostName;
            _portNumber = portNumber;
            _userName = userName;
            _password = password;
            _sslRequired = sslRequired;
        }

        /// <summary>
        /// Set up the mail message to be sent.
        /// </summary>
        /// <param name="subject">Set the subject of the email</param>
        /// <param name="toEmailAddresses">Set the email addresses the mail should be sent to.</param>
        /// <param name="fromMailAddress">Set the sender of the email</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public IEmailMessage Message(string subject, IEnumerable<MailAddress> toEmailAddresses, MailAddress fromMailAddress)
        {
            if (string.IsNullOrEmpty(subject)) throw new ArgumentNullException(nameof(subject));

            if (toEmailAddresses == null || toEmailAddresses.Count() == 0) throw new ArgumentNullException(nameof(toEmailAddresses));

            if (fromMailAddress == null || string.IsNullOrWhiteSpace(fromMailAddress.Address)) throw new ArgumentNullException(nameof(fromMailAddress));

            _mailMessage = new EmailMessage(subject, _hostServer, _portNumber, _userName, _password, _sslRequired, toEmailAddresses);

            return _mailMessage;
        }

        /// <summary>
        /// Set up the mail message to be sent.
        /// </summary>
        /// <param name="subject">Set the subject of the email</param>
        /// <param name="toEmailAddresses">Set the email addresses the mail should be sent to.</param>
        /// <param name="fromMailAddress">Set the sender of the email</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException">Thrown when the email address is not in the right format.</exception>
        public IEmailMessage Message(string subject, IEnumerable<string> toEmailAddresses, string fromMailAddress)
        {
            if (string.IsNullOrEmpty(subject)) throw new ArgumentNullException(nameof(subject));

            if (toEmailAddresses == null || toEmailAddresses.Count() == 0) throw new ArgumentNullException(nameof(toEmailAddresses));

            if (fromMailAddress == null || string.IsNullOrWhiteSpace(fromMailAddress)) throw new ArgumentNullException(nameof(fromMailAddress));

            _mailMessage = new EmailMessage(subject, _hostServer, _portNumber, _userName, _password, _sslRequired, toEmailAddresses.Select(address => new MailAddress(address)).ToList());

            return _mailMessage;
        }
    }
}