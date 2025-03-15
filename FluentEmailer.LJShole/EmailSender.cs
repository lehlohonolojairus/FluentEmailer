using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using FluentEmailer.LJShole.Interfaces;

namespace FluentEmailer.LJShole
{
    public class EmailSender : IEmailSender
    {
        private readonly string _hostServer;
        private readonly string _portNumber;
        private readonly string _userName;
        private readonly string _password;
        private readonly bool _sslRequired;
        private readonly MailMessage _message;
        private NetworkCredential? _networkCredential;

        internal EmailSender(MailMessage message, string hostServer, string portNumber, string userName, string password, bool sslRequired)
        {
            _message = message;
            _hostServer = hostServer;
            _portNumber = portNumber;
            _userName = userName;
            _password = password;
            _sslRequired = sslRequired;
        }

        /// <summary>
        /// Sends out the email.
        /// </summary>
        /// <returns></returns>
        public bool Send()
        {
            try
            {
                _networkCredential = new NetworkCredential(_userName, _password);
                SmtpClient smtpClient = new SmtpClient(_hostServer, int.Parse(_portNumber))
                {
                    Credentials = _networkCredential,
                    EnableSsl = _sslRequired,
                    Host = _hostServer,
                    Port = int.Parse(_portNumber),
                };
                smtpClient.Send(_message);
                return true;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Sends out the email asynchronously.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SendAsync()
        {
            return await Task.Run(() =>
            {
                _networkCredential = new NetworkCredential(_userName, _password);
                SmtpClient smtpClient = new SmtpClient(_hostServer, int.Parse(_portNumber))
                {
                    Credentials = _networkCredential,
                    EnableSsl = _sslRequired,
                    Host = _hostServer,
                    Port = int.Parse(_portNumber),
                };
                smtpClient.Send(_message);
                return true;
            });
        }
    }
}