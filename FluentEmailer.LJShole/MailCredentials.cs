namespace FluentEmailer.LJShole
{
    public class MailCredentials
    {
       internal string _hostServer;
       internal string _portNumber;
       internal string _userName;
       internal string _password;
       internal bool _serverRequiresSsl;
       internal Mailer _mailer;

        public MailCredentials(Mailer mailer)
        {
            _mailer = mailer;
        }
        /// <summary>
        /// The SMTP / IMAP server.
        /// </summary>
        /// <param name="hostServer"></param>
        /// <returns></returns>
        public MailCredentials UsingHostServer(string hostServer)
        {
            _hostServer = hostServer;
            return this;
        }
        /// <summary>
        /// The port number to use when sending emails.
        /// </summary>
        /// <param name="portNumber">The port number.</param>
        /// <returns></returns>
        public MailCredentials OnPortNumber(string portNumber)
        {
            _portNumber = portNumber;
            return this;
        }
        /// <summary>
        /// The email address to use for sending the email from.
        /// </summary>
        /// <param name="userName">The email address as configured in the SMTP/IMAP server.</param>
        /// <returns></returns>
        public MailCredentials WithUserName(string userName)
        {
            _userName = userName;
            return this;
        }

        public MailCredentials HostServerRequresSsl(bool serverRequiresSsl)
        {
            _serverRequiresSsl = serverRequiresSsl;
            return this;
        }
        /// <summary>
        /// Set the password associated with the userName / email address.
        /// </summary>
        /// <param name="password">The password associated with the email address to use when sending emails.</param>
        /// <returns></returns>
        public Mailer WithPassword(string password)
        {
            _password = password;
            return _mailer;
        }
    }
}
