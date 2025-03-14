using FluentEmailer.LJShole.Interfaces;
using System;

namespace FluentEmailer.LJShole
{
    /// <summary>
    /// The main entry point of the package. Starts up the process of crafting your email(s).
    /// </summary>
    public class FluentEmail : IFluentEmail
    {
        private ISMTPServer _smtpServer;

        private ISendGridServer _sendGridServer;

        /// <summary>
        /// Bootstraps the process of sending an email using your SMTP or IMAP server.
        /// </summary>
        /// <param name="hostServer">Host Server the email will be sent from.</param>
        /// <param name="portNumber">The port number.</param>
        /// <param name="userName">The username used to connect to the host server.</param>
        /// <param name="password">The password corresponding to the username connecting with to the host server.</param>
        /// <param name="sslRequired">Indicate if the connection to the server required SSL or not, default is true.</param>
        /// <returns></returns>
        public ISMTPServer UsingSMTPServer(string hostServer, string portNumber, string userName, string password, bool sslRequired = true)
        {
            if(string.IsNullOrWhiteSpace(hostServer)) throw new ArgumentNullException(nameof(hostServer));
            if(string.IsNullOrWhiteSpace(portNumber)) throw new ArgumentNullException(nameof(portNumber));
            if(string.IsNullOrWhiteSpace(userName)) throw new ArgumentNullException(nameof(userName));
            if(string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));
            
            _smtpServer = new SMTPServer(hostServer, portNumber, userName, password, sslRequired);

            return _smtpServer;
        }
        public ISMTPServer UsingSendGrid(string ApiKey)
        {
            throw new NotImplementedException();
        }

    }
}