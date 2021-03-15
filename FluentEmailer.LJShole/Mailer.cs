using FluentEmailer.LJShole.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FluentEmailer.LJShole
{
    /// <summary>
    /// Class to use for sending emails.
    /// </summary>
    public class Mailer : IMailer
    {
        private IEmailMessage _mailMessage;
        private IMailCredentials _mailCredentials;
        private NetworkCredential _networkCredential;

        public Mailer()
        {

        }

        public Mailer(IMailCredentials mailCredentialSettings)
        {
            _mailCredentials = mailCredentialSettings;
        }

        /// <summary>
        /// Bootstrap the email creation process.
        /// </summary>
        /// <returns></returns>
        public IEmailMessage SetUpMessage()
        {
            _mailMessage = _mailMessage ?? new EmailMessage(this);

            return _mailMessage;
        }

        /// <summary>
        /// Sends out the email.
        /// </summary>
        /// <returns></returns>
        public bool Send()
        {
            _networkCredential = _networkCredential ?? new NetworkCredential(_mailCredentials.UserName, _mailCredentials.Password);
            var mailMsg = _mailMessage.GetMessage();
            SmtpClient smtpClient = new SmtpClient(_mailCredentials.HostServer, int.Parse(_mailCredentials.PortNumber))
            {
                Credentials = _networkCredential,
                EnableSsl = _mailCredentials.HostServerRequiresSsl
            };
            smtpClient.Send(mailMsg);
            return true;
        }

        /// <summary>
        /// Sends out the email.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SendAsync()
        {
            return await Task.Run(() =>
                  {
                      _networkCredential = _networkCredential ?? new NetworkCredential(_mailCredentials.UserName, _mailCredentials.Password);
                      var mailMsg = _mailMessage.GetMessage();
                      SmtpClient smtpClient = new SmtpClient(_mailCredentials.HostServer, int.Parse(_mailCredentials.PortNumber))
                      {
                          Credentials = _networkCredential,
                          EnableSsl = _mailCredentials.HostServerRequiresSsl
                      };
                      smtpClient.Send(mailMsg);
                      return true;
                  });
        }

        /// <summary>
        /// Returns the message instance.
        /// </summary>
        public IEmailMessage Message { get { return _mailMessage; } }
        /// <summary>
        /// Returns the specified login mail credentials as configured on the SMTP / IMAP server.
        /// </summary>
        public IMailCredentials MailCredentials { get { return _mailCredentials; } }

        internal void SetMailCredentials(IMailCredentials mailCredentials)
        {
            _mailCredentials = mailCredentials;
        }
        internal void SetNetworkCredential(NetworkCredential networkCredential)
        {
            _networkCredential = networkCredential;
        }

    }
}
