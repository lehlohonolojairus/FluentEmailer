using System.Net;
using System.Threading.Tasks;

namespace FluentEmailer.LJShole.Interfaces
{
    public interface IMailer
    {
        IEmailMessage SetUpMessage();
        IEmailMessage Message { get; }
        IMailCredentials MailCredentials { get; }
        bool Send();
        Task<bool> SendAsync();
    }
}