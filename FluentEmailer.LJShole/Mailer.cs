using System;
using System.Linq;
using System.Net.Mail;

namespace FluentEmailer.LJShole
{
    public class Mailer
    {
        public EmailMessage _mailMessage;

        public EmailMessage Message()
        {
            _mailMessage = _mailMessage ?? new EmailMessage(this);

            return _mailMessage;
        }

        public bool Send()
        {
            try
            {
                MailMessage mailMsg = CreateMessageMailMessage();
                SmtpClient smtpClient = new SmtpClient(_mailMessage._mailCredentials._hostServer, int.Parse(_mailMessage._mailCredentials._portNumber));
                System.Net.NetworkCredential credentials =
                   new System.Net.NetworkCredential(_mailMessage._mailCredentials._userName, _mailMessage._mailCredentials._password);
                smtpClient.Credentials = credentials;
                smtpClient.Send(mailMsg);
                return true;
            }
            catch (ArgumentNullException ex)
            {
                // message is null
            }
            catch (ObjectDisposedException ex)
            {
                //     This object has been disposed.
            }
            catch (SmtpFailedRecipientsException ex)
            {
                //     The message could not be delivered to two or more of the recipients in System.Net.Mail.MailMessage.To,
                //     System.Net.Mail.MailMessage.CC, or System.Net.Mail.MailMessage.Bcc.
            }
            catch (SmtpFailedRecipientException ex)
            {
                //     The message could not be delivered to one of the recipients in System.Net.Mail.MailMessage.To,
                //     System.Net.Mail.MailMessage.CC, or System.Net.Mail.MailMessage.Bcc.
                //
            }
            catch (SmtpException ex)
            {
                //     The connection to the SMTP server failed. - or - Authentication failed. - or - The
                //     operation timed out. -or- System.Net.Mail.SmtpClient.EnableSsl is set to true
                //     but the System.Net.Mail.SmtpClient.DeliveryMethod property is set to System.Net.Mail.SmtpDeliveryMethod.SpecifiedPickupDirectory
                //     or System.Net.Mail.SmtpDeliveryMethod.PickupDirectoryFromIis. -or- System.Net.Mail.SmtpClient.EnableSsl
                //     is set to true, but the SMTP mail server did not advertise STARTTLS in the response
                //     to the EHLO command.
            }
            catch (InvalidOperationException ex)
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }

        private MailMessage CreateMessageMailMessage()
        {
            var mailMsg = new MailMessage();

            mailMsg.From = _mailMessage._fromMailAddress;

            _mailMessage._toMailAddresses.ToList().ForEach(mailAddress =>
            {
                mailMsg.To.Add(mailAddress);
            });
            _mailMessage._ccMailAddresses.ToList().ForEach(mailAddress =>
            {
                mailMsg.CC.Add(mailAddress);
            });
            _mailMessage._bccMailAddresses.ToList().ForEach(mailAddress =>
            {
                mailMsg.Bcc.Add(mailAddress);
            });

            mailMsg.Subject = _mailMessage._subject;
            mailMsg.Priority = MailPriority.High;
            mailMsg.IsBodyHtml = _mailMessage._emailBodyIsHtml;
            mailMsg.Body = _mailMessage._body;

            return mailMsg;
        }
    }
}
