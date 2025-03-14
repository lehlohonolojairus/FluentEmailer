using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace FluentEmailer.LJShole.Interfaces
{
    public interface IEmailMessage
    {
        IEmailMessage BccMailAddresses(IEnumerable<MailAddress> bccMailAddresses);
        IEmailMessage CcMailAddresses(IEnumerable<MailAddress> ccMailAddresses);
        IEmailMessage AddAttachments(IEnumerable<Attachment> attachments);
        IEmailSender Body(string messageBody, bool bodyIsHTML = true);
        /// <summary>
        /// Create message body using template values. This will require a mail template file to be specified. 
        /// </summary>
        /// <param name="templateValues"></param>
        /// <param name="bodyIsHTML"></param>
        /// <returns></returns>
        IEmailSender Body(IDictionary<string, string> templateValues, string TemplateFileLocation, bool bodyIsHTML = true);
        IEmailMessage SubjectEncoding(Encoding encoding);
        IEmailMessage SetPriority(MailPriority mailPriority);
        IEmailMessage ReplyTo(MailAddress replyToEmail);
        IEmailMessage BodyEncoding(Encoding encoding);
        IEmailMessage BodyTransferEncoding(TransferEncoding transferEncoding);
        IEmailMessage FromMailAddresses(MailAddress fromMailAddress);
    }
}