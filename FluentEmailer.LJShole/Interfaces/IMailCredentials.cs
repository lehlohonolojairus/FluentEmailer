namespace FluentEmailer.LJShole.Interfaces
{
    public interface IMailCredentials
    {
        IMailCredentials HostServerRequresSsl(bool serverRequiresSsl);
        bool HostServerRequiresSsl { get; }
        IMailCredentials OnPortNumber(string portNumber);
        string PortNumber { get; }
        IMailCredentials UsingHostServer(string hostServer);
        string HostServer { get; }
        IMailer WithPassword(string password);
        string Password { get; }
        IMailCredentials WithUserName(string userName);
        string UserName { get; }

    }
}