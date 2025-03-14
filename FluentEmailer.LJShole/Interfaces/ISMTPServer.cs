using System.Collections.Generic;
using System.Net.Mail;

namespace FluentEmailer.LJShole.Interfaces
{
    public interface ISMTPServer
    {
        IEmailMessage Message(string subject, IEnumerable<MailAddress> toEmailAddresses, MailAddress fromMailAddress);
        IEmailMessage Message(string subject, IEnumerable<string> toEmailAddresses, string fromMailAddress);
    }
}