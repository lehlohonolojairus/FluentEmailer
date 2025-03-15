using System.Threading.Tasks;

namespace FluentEmailer.LJShole.Interfaces
{
    /// <summary>
    /// Sends the email composed. 
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Sends the email.
        /// </summary>
        /// <returns></returns>
        bool Send();

        /// <summary>
        /// Sends the email asynchronously
        /// </summary>
        /// <returns></returns>
        Task<bool> SendAsync();
    }
}