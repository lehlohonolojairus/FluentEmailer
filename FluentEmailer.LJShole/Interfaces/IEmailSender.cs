using System.Threading.Tasks;

namespace FluentEmailer.LJShole.Interfaces
{
    public interface IEmailSender
    {
        bool Send();
        Task<bool> SendAsync();
    }
}