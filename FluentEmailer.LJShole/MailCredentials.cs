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
