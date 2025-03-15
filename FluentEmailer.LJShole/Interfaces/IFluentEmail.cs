using FluentEmailer.LJShole.EmailChannel.SMTP;

namespace FluentEmailer.LJShole.Interfaces
{
    /// <summary>
    /// The main entry point of the package. Starts up the process of crafting your email(s).
    /// </summary>
    public interface IFluentEmail
    {
        /// <summary>
        /// Bootstraps the process of sending an email using your SMTP or IMAP server.
        /// </summary>
        /// <param name="hostServer">Host Server the email will be sent from.</param>
        /// <param name="portNumber">The port number.</param>
        /// <param name="userName">The username used to connect to the host server.</param>
        /// <param name="password">The password corresponding to the username connecting with to the host server.</param>
        /// <param name="sslRequired">Indicate if the connection to the server required SSL or not, default is true.</param>
        /// <returns></returns>
        ISMTPServer UsingSMTPServer(string hostServer, string portNumber, string userName, string password, bool sslRequired = true);
    }
}