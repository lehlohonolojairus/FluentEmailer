namespace FluentEmailer.LJShole
{
    public class MailCredentials
    {
        public string _hostServer;
        public string _portNumber;
        public string _userName;
        public string _password;
        public bool _serverRequiresSsl;
        public Mailer _mailer;

        public MailCredentials(Mailer mailer)
        {
            _mailer = mailer;
        }
        public MailCredentials UsingHostServer(string hostServer)
        {
            _hostServer = hostServer;
            return this;
        }
        public MailCredentials OnPortNumber(string portNumber)
        {
            _portNumber = portNumber;
            return this;
        }
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
        public Mailer WithPassword(string password)
        {
            _password = password;
            return _mailer;
        }
    }
}
