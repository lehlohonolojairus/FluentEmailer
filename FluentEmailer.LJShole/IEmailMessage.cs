using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace FluentEmailer.LJShole
{
    public interface IEmailMessage
    {
        IEmailMessage AddBccMailAddresses(List<MailAddress> bccMailAddresses);
        IEmailMessage AddCcMailAddresses(List<MailAddress> ccMailAddresses);
        IEmailMessage AddFromMailAddresses(MailAddress fromMailAddress);
        IEmailMessage AddToMailAddresses(List<MailAddress> toMailAddresses);
        IEmailMessage SetBodyIsHtmlFlag(bool emailBodyIsHtml = true);
        IEmailMessage WithAttachments(List<Attachment> attachments);
        IEmailTemplate WithBody();
        IMailCredentials WithCredentials();
        IEmailMessage WithSubject(string subject);
        MailMessage GetMessage();
    }
}