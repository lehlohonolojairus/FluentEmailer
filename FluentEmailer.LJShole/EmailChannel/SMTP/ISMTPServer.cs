using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace FluentEmailer.LJShole.EmailChannel.SMTP
{
    /// <summary>
    /// Send emails using SMTP/IMAP Server
    /// </summary>
    public interface ISMTPServer
    {
        /// <summary>
        /// Set up the mail message to be sent.
        /// </summary>
        /// <param name="subject">Set the subject of the email</param>
        /// <param name="toEmailAddresses">Set the email addresses the mail should be sent to.</param>
        /// <param name="fromMailAddress">Set the sender of the email</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        IEmailMessage Message(string subject, IEnumerable<MailAddress> toEmailAddresses, MailAddress fromMailAddress);

        /// <summary>
        /// Set up the mail message to be sent.
        /// </summary>
        /// <param name="subject">Set the subject of the email</param>
        /// <param name="toEmailAddresses">Set the email addresses the mail should be sent to.</param>
        /// <param name="fromMailAddress">Set the sender of the email</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException">Thrown when the email address is not in the right format.</exception>
        IEmailMessage Message(string subject, IEnumerable<string> toEmailAddresses, string fromMailAddress);
    }
}