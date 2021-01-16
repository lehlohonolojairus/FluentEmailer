using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace FluentEmailer.LJShole.Interfaces
{
    public interface IEmailMessage
    {
        IEmailMessage AddBccMailAddresses(List<MailAddress> bccMailAddresses);
        IEmailMessage AddCcMailAddresses(List<MailAddress> ccMailAddresses);
        IEmailMessage AddFromMailAddresses(MailAddress fromMailAddress);
        IEmailMessage AddToMailAddresses(List<MailAddress> toMailAddresses);       
        IEmailMessage WithAttachments(List<Attachment> attachments);
        IEmailBody SetUpBody();      
        IMailCredentials WithCredentials();
        IEmailMessage WithSubject(string subject);
        IEmailMessage SetSubjectEncoding(Encoding encoding);
        IEmailMessage SetPriority(MailPriority mailPriority);
        IEmailMessage WithReplyTo(MailAddress replyToEmail) ;        
        MailMessage GetMessage();
    }
    public interface IEmailBody
    {
        IEmailTemplate Body();
        IEmailBody SetBodyTransferEncoding(TransferEncoding transferEncoding);
        IEmailBody SetBodyEncoding(Encoding encoding);
        IEmailMessage SetBodyIsHtmlFlag(bool emailBodyIsHtml = true);
    }
}