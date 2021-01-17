using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Xunit;

namespace FluentEmailer.LJShole.Tests
{
    public class MailerTests
    {
        readonly string hostName = "mail.creativemode.co.za";
        readonly string portNumber = "25";
        readonly string userName = "admin@creativemode.co.za";
        readonly string password = "jhja%&j2*&jdjaHGAmxh";
        readonly string toEmail = "ljshole@gmail.com";
        readonly string ccEmail = "ljshole@yahoo.com";
        readonly string bccEmail = "hloni@creativemode.co.za";

        [Fact]
        public void Can_Send_Mail_With_String_Body_With_No_Attachment()
        {
            var emailIsSent = new Mailer()
                    .SetUpMessage()
                        .Subject("Mail Subject")
                        .FromMailAddresses(new MailAddress(userName, "Fluent Email - No Attachments"))
                        .ToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
                        .SetUpBody()
                            .SetBodyEncoding(Encoding.UTF8)
                            .SetBodyTransferEncoding(TransferEncoding.Unknown)
                            .Body()
                                .UsingString("This is me Testing")
                                .SetBodyIsHtmlFlag()
                   .SetPriority(MailPriority.Normal)
                   .WithCredentials()
                        .UsingHostServer(hostName)
                        .OnPortNumber(portNumber)
                        .WithUserName(userName)
                        .WithPassword(password)
                    .Send();

            Assert.True(emailIsSent);
        }

        [Fact]
        public void Can_Send_Mail_With_String_Body_With_Attachment()
        {
            var emailIsSent = new Mailer()
                               .SetUpMessage()
                                    .Subject("Mail Subject")
                                    .FromMailAddresses(new MailAddress(userName, "Fluent Email - With Attachments"))
                                    .ToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
                                    .WithTheseAttachments(new List<Attachment> { new Attachment($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SamplePDF.pdf")}") })
                                   .SetUpBody()
                                       .SetBodyEncoding(Encoding.UTF8)
                                       .SetBodyTransferEncoding(TransferEncoding.Unknown)
                                       .Body()
                                           .UsingString("This is me Testing")
                                           .SetBodyIsHtmlFlag()
                              .SetPriority(MailPriority.Normal)
                              .WithCredentials()
                                   .UsingHostServer(hostName)
                                   .OnPortNumber(portNumber)
                                   .WithUserName(userName)
                                   .WithPassword(password)
                               .Send();

            Assert.True(emailIsSent);
        }

        [Fact]
        public void Can_Send_Mail_With_String_Body_Add_BccAndCcEmails()
        {
            var emailIsSent = new Mailer()
                              .SetUpMessage()
                                   .Subject("Mail Subject")
                                   .FromMailAddresses(new MailAddress(userName, "Fluent Email - No Attachments - Bcc and CC Emails"))
                                   .ToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
                                   .CcMailAddresses(new List<MailAddress> { new MailAddress(ccEmail) })
                                   .BccMailAddresses(new List<MailAddress> { new MailAddress(bccEmail) })
                                   .SetUpBody()
                                      .SetBodyEncoding(Encoding.UTF8)
                                      .SetBodyTransferEncoding(TransferEncoding.Unknown)
                                      .Body()
                                          .UsingString("Email body as string")
                                          .SetBodyIsHtmlFlag()
                             .SetPriority(MailPriority.Normal)
                             .WithCredentials()
                                  .UsingHostServer(hostName)
                                  .OnPortNumber(portNumber)
                                  .WithUserName(userName)
                                  .WithPassword(password)
                              .Send();

            Assert.True(emailIsSent);
        }

        [Fact]
        public void Can_Send_Mail_Using_Email_Template_With_No_Attachments()
        {
            var emailIsSent = new Mailer()
                            .SetUpMessage()
                                 .Subject("Mail Subject Template")
                                 .FromMailAddresses(new MailAddress(userName, "Fluent Email - Template - No Attachments"))
                                 .ToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
                                 .SetUpBody()
                                    .SetBodyEncoding(Encoding.UTF8)
                                    .SetBodyTransferEncoding(TransferEncoding.Unknown)
                                    .Body()
                                         .UsingEmailTemplate($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestHtmlPage.html")}")
                                         .UsingTemplateDictionary(new Dictionary<string, string> { { "{{subject}}", "Testing Message" }, { "{{body}}", "<section><h2>This is</h2><p>Welcome to our world</p></section>" } })
                                         .CompileTemplate()
                                         .SetBodyIsHtmlFlag()
                           .SetPriority(MailPriority.Normal)
                           .WithCredentials()
                                .UsingHostServer(hostName)
                                .OnPortNumber(portNumber)
                                .WithUserName(userName)
                                .WithPassword(password)
                            .Send();

            Assert.True(emailIsSent);
        }

        [Fact]
        public void Can_Send_Mail_Using_Email_Template_With_Attachments()
        {
            var emailIsSent = new Mailer()
                            .SetUpMessage()
                                 .Subject("Mail Subject Template")
                                 .FromMailAddresses(new MailAddress(userName, "Fluent Email - Template - With Attachments"))
                                 .ToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
                                 .WithTheseAttachments(new List<Attachment> { new Attachment($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SamplePDF.pdf")}") })
                                 .SetUpBody()
                                    .SetBodyEncoding(Encoding.UTF8)
                                    .SetBodyTransferEncoding(TransferEncoding.Unknown)
                                    .Body()
                                         .UsingEmailTemplate($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestHtmlPage.html")}")
                                         .UsingTemplateDictionary(new Dictionary<string, string> { { "{{subject}}", "Testing Message" }, { "{{body}}", "<section><h2>This is</h2><p>Welcome to our world</p></section>" } })
                                         .CompileTemplate()
                                         .SetBodyIsHtmlFlag()
                           .SetPriority(MailPriority.Normal)
                           .WithCredentials()
                                .UsingHostServer(hostName)
                                .OnPortNumber(portNumber)
                                .WithUserName(userName)
                                .WithPassword(password)
                            .Send();

            Assert.True(emailIsSent);
        }

        [Fact]
        public void Throws_ArgumentNullException_When_Email_Subject_Is_Not_Provided()
        {
            var response = Assert.Throws<ArgumentNullException>(() => new Mailer()
                    .SetUpMessage()
                        .Subject(string.Empty)
                        .FromMailAddresses(new MailAddress(userName, "Fluent Email - With Attachments"))
                        .ToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
                        .SetUpBody()
                            .Body()
                                .UsingString("Test Body")
                                .SetBodyIsHtmlFlag()
                   .WithCredentials()
                        .UsingHostServer(hostName)
                        .OnPortNumber(portNumber)
                        .WithUserName(userName)
                        .WithPassword(password)
                    .Send());

            Assert.True(response.Message.Equals($"Value cannot be null. (Parameter 'subject')"));
        }

        [Fact]
        public void Throws_ArgumentNullException_When_To_Email_Is_Not_Provided()
        {
            var response = Assert.Throws<ArgumentNullException>(() => new Mailer()
                    .SetUpMessage()
                        .Subject("Test Subject")
                        .FromMailAddresses(new MailAddress(userName, "Fluent Email - With Attachments"))
                        .ToMailAddresses(null)
                        .SetUpBody()
                            .Body()
                                .UsingString("Test Body")
                                .SetBodyIsHtmlFlag()
                   .WithCredentials()
                        .UsingHostServer(hostName)
                        .OnPortNumber(portNumber)
                        .WithUserName(userName)
                        .WithPassword(password)
                    .Send());

            Assert.True(response.Message.Equals($"Value cannot be null. (Parameter 'toMailAddresses')"));
        }

        [Fact]
        public void Throws_ArgumentNullException_When_Cc_Email_Is_Not_Provided()
        {
            var response = Assert.Throws<ArgumentNullException>(() => new Mailer()
                    .SetUpMessage()
                        .Subject("Test Subject")
                        .FromMailAddresses(new MailAddress(userName, "Fluent Email - With Attachments"))
                        .ToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
                        .CcMailAddresses(null)
                        .SetUpBody()
                            .Body()
                                .UsingString("Test Body")
                                .SetBodyIsHtmlFlag()
                   .WithCredentials()
                        .UsingHostServer(hostName)
                        .OnPortNumber(portNumber)
                        .WithUserName(userName)
                        .WithPassword(password)
                    .Send());

            Assert.True(response.Message.Equals($"Value cannot be null. (Parameter 'ccMailAddresses')"));
        }

        [Fact]
        public void Throws_ArgumentNullException_When_Bcc_Email_Is_Not_Provided()
        {
            var response = Assert.Throws<ArgumentNullException>(() => new Mailer()
                    .SetUpMessage()
                        .Subject("Test Subject")
                        .FromMailAddresses(new MailAddress(userName, "Fluent Email - With Attachments"))
                        .ToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
                        .BccMailAddresses(null)
                        .SetUpBody()
                            .Body()
                                .UsingString("Test Body")
                                .SetBodyIsHtmlFlag()
                   .WithCredentials()
                        .UsingHostServer(hostName)
                        .OnPortNumber(portNumber)
                        .WithUserName(userName)
                        .WithPassword(password)
                    .Send());

            Assert.True(response.Message.Equals($"Value cannot be null. (Parameter 'bccMailAddresses')"));
        }

        [Fact]
        public void Throws_ArgumentNullException_When_From_Email_Is_Not_Provided()
        {
            var response = Assert.Throws<ArgumentNullException>(() => new Mailer()
                    .SetUpMessage()
                        .Subject("Test Subject")
                        .FromMailAddresses(null)
                        .ToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
                        .SetUpBody()
                           .Body()
                             .UsingString("Test Body")
                             .SetBodyIsHtmlFlag()
                   .WithCredentials()
                        .UsingHostServer(hostName)
                        .OnPortNumber(portNumber)
                        .WithUserName(userName)
                        .WithPassword(password)
                    .Send());

            Assert.True(response.Message.Equals($"Value cannot be null. (Parameter 'fromMailAddress')"));
        }

        [Fact]
        public void Can_Send_Mail_With_ReplyTo_SubjectEncoding_BodyEncoding_BodyTranser_Set()
        {
            var emailIsSent = new Mailer()
                    .SetUpMessage()
                        .Subject("Mail Subject")
                        .FromMailAddresses(new MailAddress(userName, "Fluent Email - No Attachments - With ReplyTo"))
                        .ToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
                        .SubjectEncoding(Encoding.UTF8)
                        .ReplyTo(new MailAddress("info@creativemode.co.za"))
                        .SetUpBody()
                            .SetBodyEncoding(Encoding.UTF8)
                            .SetBodyTransferEncoding(TransferEncoding.Unknown)
                            .Body()
                                .UsingString("This is me Testing")
                                .SetBodyIsHtmlFlag()
                   .SetPriority(MailPriority.Normal)
                   .WithCredentials()
                        .UsingHostServer(hostName)
                        .OnPortNumber(portNumber)
                        .WithUserName(userName)
                        .WithPassword(password)
                    .Send();

            Assert.True(emailIsSent);
        }
        [Fact]
        public void Can_Send_Mail_Using_Injected_Credentials()
        {
            var emailIsSent = new Mailer(new MailCredentials { PortNumber = portNumber, HostServer = hostName, Password = password, UserName = userName })
                    .SetUpMessage()
                        .Subject("Mail Subject")
                        .FromMailAddresses(new MailAddress(userName, "Fluent Email - Injected  Credentials"))
                        .ToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
                        .SubjectEncoding(Encoding.UTF8)
                        .ReplyTo(new MailAddress("info@creativemode.co.za"))
                        .SetUpBody()
                            .SetBodyEncoding(Encoding.UTF8)
                            .SetBodyTransferEncoding(TransferEncoding.Unknown)
                            .Body()
                                .UsingString("This is me Testing")
                                .SetBodyIsHtmlFlag()
                   .SetPriority(MailPriority.Normal)
                   .UsingTheInjectedCredentials()
                   .Send();

            Assert.True(emailIsSent);
        }
    }
}
