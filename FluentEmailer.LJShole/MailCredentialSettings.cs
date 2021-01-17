using FluentEmailer.LJShole.Interfaces;

namespace FluentEmailer.LJShole
{
    public class MailCredentialSettings : IMailCredentialSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PortNumber { get; set; }
        public string HostServer { get; set; }
    }
}
