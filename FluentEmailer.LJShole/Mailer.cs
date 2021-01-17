using FluentEmailer.LJShole.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace FluentEmailer.LJShole
{
    /// <summary>
    /// Class to use for sending emails.
    /// </summary>
    public class Mailer : IMailer
    {
        private IEmailMessage _mailMessage;
        private IMailCredentials _mailCredentials;
        private NetworkCredential _networkCredential;

        public Mailer()
        {

        }

        public Mailer(IMailCredentials mailCredentialSettings)
        {
            _mailCredentials = mailCredentialSettings;
        }
       
        /// <summary>
        /// Bootstrap the email creation process.
        /// </summary>
        /// <returns></returns>
        public IEmailMessage SetUpMessage()
        {
            _mailMessage = _mailMessage ?? new EmailMessage(this);

            return _mailMessage;
        }

        /// <summary>
        /// Sends out the email.
        /// </summary>
        /// <returns></returns>
        public bool Send()
        {
            try
            {
                var mailMsg = _mailMessage.GetMessage();
                SmtpClient smtpClient = new SmtpClient(_mailCredentials.HostServer, int.Parse(_mailCredentials.PortNumber))
                {
                    Credentials = _networkCredential,
                    EnableSsl = _mailCredentials.HostServerRequiresSsl
                };
                smtpClient.Send(mailMsg);
                return true;
            }
            catch (ArgumentNullException)
            {
                // message is null
                throw;
            }
            catch (ObjectDisposedException)
            {
                //     This object has been disposed.
                throw;
            }
            catch (SmtpFailedRecipientsException)
            {
                //     The message could not be delivered to two or more of the recipients in System.Net.Mail.MailMessage.To,
                //     System.Net.Mail.MailMessage.CC, or System.Net.Mail.MailMessage.Bcc.
                throw;
            }
            catch (SmtpFailedRecipientException)
            {
                //     The message could not be delivered to one of the recipients in System.Net.Mail.MailMessage.To,
                //     System.Net.Mail.MailMessage.CC, or System.Net.Mail.MailMessage.Bcc.
                throw;
            }
            catch (SmtpException)
            {
                //     The connection to the SMTP server failed. - or - Authentication failed. - or - The
                //     operation timed out. -or- System.Net.Mail.SmtpClient.EnableSsl is set to true
                //     but the System.Net.Mail.SmtpClient.DeliveryMethod property is set to System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory
                //     or System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis. -or- System.Net.Mail.SmtpClient.EnableSsl
                //     is set to true, but the SMTP mail server did not advertise STARTTLS in the response
                //     to the EHLO command.
                throw;
            }
            catch (InvalidOperationException)
            {
                //     This System.Net.Mail.SmtpClient has a Overload:System.Net.Mail.SmtpClient.SendAsync
                //     call in progress. -or- System.Net.Mail.MailMessage.From is null. -or- There are
                //     no recipients specified in System.Net.Mail.MailMessage.To, System.Net.Mail.MailMessage.CC,
                //     and System.Net.Mail.MailMessage.Bcc properties. -or- System.Net.Mail.SmtpClient.DeliveryMethod
                //     property is set to System.Net.Mail.SmtpDeliveryMethod.Network and System.Net.Mail.SmtpClient.Host
                //     is null. -or- System.Net.Mail.SmtpClient.DeliveryMethod property is set to System.Net.Mail.SmtpDeliveryMethod.Network
                //     and System.Net.Mail.SmtpClient.Host is equal to the empty string (""). -or- System.Net.Mail.SmtpClient.DeliveryMethod
                //     property is set to System.Net.Mail.SmtpDeliveryMethod.Network and System.Net.Mail.SmtpClient.Port
                //     is zero, a negative number, or greater than 65,535.
                throw;
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Sends out the email.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SendAsync()
        {
            try
            {
                return await Task.Run(() =>
                {
                    var mailMsg = _mailMessage.GetMessage();
                    SmtpClient smtpClient = new SmtpClient(_mailCredentials.HostServer, int.Parse(_mailCredentials.PortNumber))
                    {
                        Credentials = _networkCredential,
                        EnableSsl = _mailCredentials.HostServerRequiresSsl
                    };
                    smtpClient.Send(mailMsg);
                    return true;
                });
            }
            catch (ArgumentNullException)
            {
                // message is null
                throw;
            }
            catch (ObjectDisposedException)
            {
                //     This object has been disposed.
                throw;
            }
            catch (SmtpFailedRecipientsException)
            {
                //     The message could not be delivered to two or more of the recipients in System.Net.Mail.MailMessage.To,
                //     System.Net.Mail.MailMessage.CC, or System.Net.Mail.MailMessage.Bcc.
                throw;
            }
            catch (SmtpFailedRecipientException)
            {
                //     The message could not be delivered to one of the recipients in System.Net.Mail.MailMessage.To,
                //     System.Net.Mail.MailMessage.CC, or System.Net.Mail.MailMessage.Bcc.
                throw;
            }
            catch (SmtpException)
            {
                //     The connection to the SMTP server failed. - or - Authentication failed. - or - The
                //     operation timed out. -or- System.Net.Mail.SmtpClient.EnableSsl is set to true
                //     but the System.Net.Mail.SmtpClient.DeliveryMethod property is set to System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory
                //     or System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis. -or- System.Net.Mail.SmtpClient.EnableSsl
                //     is set to true, but the SMTP mail server did not advertise STARTTLS in the response
                //     to the EHLO command.
                throw;
            }
            catch (InvalidOperationException)
            {
                //     This System.Net.Mail.SmtpClient has a Overload:System.Net.Mail.SmtpClient.SendAsync
                //     call in progress. -or- System.Net.Mail.MailMessage.From is null. -or- There are
                //     no recipients specified in System.Net.Mail.MailMessage.To, System.Net.Mail.MailMessage.CC,
                //     and System.Net.Mail.MailMessage.Bcc properties. -or- System.Net.Mail.SmtpClient.DeliveryMethod
                //     property is set to System.Net.Mail.SmtpDeliveryMethod.Network and System.Net.Mail.SmtpClient.Host
                //     is null. -or- System.Net.Mail.SmtpClient.DeliveryMethod property is set to System.Net.Mail.SmtpDeliveryMethod.Network
                //     and System.Net.Mail.SmtpClient.Host is equal to the empty string (""). -or- System.Net.Mail.SmtpClient.DeliveryMethod
                //     property is set to System.Net.Mail.SmtpDeliveryMethod.Network and System.Net.Mail.SmtpClient.Port
                //     is zero, a negative number, or greater than 65,535.
                throw;
            }
            catch (FileNotFoundException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns the message instance.
        /// </summary>
        public IEmailMessage Message { get { return _mailMessage; } }
        /// <summary>
        /// Returns the specified login mail credentials as configured on the SMTP / IMAP server.
        /// </summary>
        public IMailCredentials MailCredentials { get { return _mailCredentials; } }
        
        internal void SetMailCredentials(IMailCredentials mailCredentials)
        {
            _mailCredentials = mailCredentials;
        }
        internal void SetNetworkCredential(NetworkCredential networkCredential)
        {
            _networkCredential = networkCredential;
        }

    }
}
