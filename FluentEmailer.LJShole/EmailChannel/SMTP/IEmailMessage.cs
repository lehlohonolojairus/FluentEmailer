using FluentEmailer.LJShole.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace FluentEmailer.LJShole.EmailChannel.SMTP
{    
     /// <summary>
     /// Mail message to be sent.
     /// </summary>
    public interface IEmailMessage
    {
        /// <summary>
        /// To emal addresses list 
        /// </summary>
        IEnumerable<MailAddress> ToMailAddressesList { get ; }

        /// <summary>
        /// CC email addresses list
        /// </summary>
        IEnumerable<MailAddress> CcMailAddressesList { get; }

        /// <summary>
        /// BCC list email addresses.
        /// </summary>
        IEnumerable<MailAddress> BccMailAddressesList { get; }


        /// <summary>
        /// Add a list of BCC email addresses the email to be sent to.
        /// </summary>
        /// <param name="bccMailAddresses">List of BCC email addresses to receive the email.</param>
        /// <returns></returns>
        IEmailMessage BccMailAddresses(IEnumerable<MailAddress> bccMailAddresses);

        /// <summary>
        /// Add a list of CC email addresses the email to be sent to.
        /// </summary>
        /// <param name="ccMailAddresses">List of CC email addresses to receive the email.</param>
        /// <returns></returns>
        IEmailMessage CcMailAddresses(IEnumerable<MailAddress> ccMailAddresses);

        /// <summary>
        /// Allows the adding of attachments on the email.
        /// </summary>
        /// <param name="attachments">File attachments to attach to the email message.</param>
        /// <returns></returns>
        IEmailMessage AddAttachments(IEnumerable<Attachment> attachments);
        
        /// <summary>
        /// Sets the body of the email message.
        /// </summary>
        /// <param name="messageBody">Message body of the email.</param>
        /// <param name="bodyIsHTML">Flag to set the body as HTML.</param>
        /// <returns></returns>
        IEmailSender Body(string messageBody, bool bodyIsHTML = true);

        /// <summary>
        /// Sets the body of the email message. This overload uses a template file (.html or txt) along with key-value dictionary that should be used to substitute placeholders in the template.
        /// </summary>
        /// <param name="templateValues">A dictionary containing keys found in the template with values that should be replaced in the template.</param>
        /// <param name="TemplateFileLocation">File location of the template. Ensure this file is accessible by the application.</param>
        /// <param name="bodyIsHTML">Flag to set the body as HTML.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        IEmailSender Body(IDictionary<string, string> templateValues, string TemplateFileLocation, bool bodyIsHTML = true);

        /// <summary>
        /// Set the encoding type of the subject. Default is UTF8
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        IEmailMessage SubjectEncoding(Encoding encoding);

        /// <summary>
        /// Set the priority level of the email.
        /// </summary>
        /// <param name="mailPriority"></param>
        /// <returns></returns>
        IEmailMessage SetPriority(MailPriority mailPriority);

        /// <summary>
        /// Adds reply-to email address for the email.
        /// </summary>
        /// <param name="replyToEmail">The reply-to email address </param>
        /// <returns></returns>
        IEmailMessage ReplyTo(MailAddress replyToEmail);

        /// <summary>
        /// Adds reply-to email addresses for the email.
        /// </summary>
        /// <param name="replyToEmails">List of email addresses.</param>
        /// <returns></returns>
        IEmailMessage ReplyTo(MailAddressCollection replyToEmails);
        
        /// <summary>
        /// Set the encoding of the body. If you are using UTF8, no need to set this value.
        /// </summary>
        /// <param name="encoding"></param>
        /// <returns></returns>
        IEmailMessage BodyEncoding(Encoding encoding);

        /// <summary>
        /// Set the body transfer encoding. If you are using Unknown, no need to set this value.
        /// </summary>
        /// <param name="transferEncoding"></param>
        /// <returns></returns>
        IEmailMessage BodyTransferEncoding(TransferEncoding transferEncoding);

        /// <summary>
        /// Add a from email address.
        /// </summary>
        /// <param name="fromMailAddress">From email address for recepients to know where the email originated from.</param>
        /// <returns></returns>
        IEmailMessage FromMailAddress(MailAddress fromMailAddress);

        /// <summary>
        /// Add a sender email address.
        /// </summary>
        /// <param name="senderMailAddress">Sender email address.</param>
        /// <returns></returns>
        IEmailMessage SenderMailAddress(MailAddress senderMailAddress);
    }
}