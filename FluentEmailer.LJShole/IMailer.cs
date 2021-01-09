using System.Net;

namespace FluentEmailer.LJShole
{
    public interface IMailer
    {
        IEmailMessage SetUpMessage();
        IEmailMessage Message { get; }
        IMailCredentials MailCredentials { get; }
        bool Send();
    }
}