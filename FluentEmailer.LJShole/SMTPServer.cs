using FluentEmailer.LJShole.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace FluentEmailer.LJShole
{
    /// <summary>
    /// Class for sending emails using SMTP/IMAP Server
    /// </summary>
    public class SMTPServer : ISMTPServer
    {
        private string _hostServer;
        private string _portNumber;
        private string _userName;
        private string _password;
        private bool _sslRequired;

        private IEmailMessage _mailMessage;

        internal SMTPServer(string hostName, string portNumber, string userName, string password, bool sslRequired)
        {
            _hostServer = hostName;
            _portNumber = portNumber;
            _userName = userName;
            _password = password;
            _sslRequired = sslRequired;
        }

        public IEmailMessage Message(string subject, IEnumerable<MailAddress> toEmailAddresses, MailAddress fromMailAddress)
        {
            if (string.IsNullOrEmpty(subject)) throw new ArgumentNullException(nameof(subject));

            if (toEmailAddresses == null || toEmailAddresses.Count() == 0) throw new ArgumentNullException(nameof(toEmailAddresses));

            if (fromMailAddress == null || string.IsNullOrWhiteSpace(fromMailAddress.Address)) throw new ArgumentNullException(nameof(fromMailAddress));

            _mailMessage = new EmailMessage(subject, _hostServer, _portNumber, _userName, _password, _sslRequired, toEmailAddresses);

            return _mailMessage;
        }

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