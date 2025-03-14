using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using FluentEmailer.LJShole.Interfaces;

namespace FluentEmailer.LJShole
{
    public class EmailSender : IEmailSender
    {
        private string _hostServer;
        private string _portNumber;
        private string _userName;
        private string _password;
        private bool _sslRequired;
        private NetworkCredential? _networkCredential;
        private MailMessage _message { get; }

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
                    //DeliveryFormat = SmtpDeliveryFormat.International,
                    Host = _hostServer,
                    Port = int.Parse(_portNumber),
                   // UseDefaultCredentials = false,
                   // DeliveryMethod = SmtpDeliveryMethod.Network,
                };
                // smtpClient.Timeout = 100000;
                smtpClient.Send(_message);
                return true;
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Sends out the email.
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
                    EnableSsl = _sslRequired
                };
                smtpClient.Send(_message);
                return true;
            });
        }
    }
}