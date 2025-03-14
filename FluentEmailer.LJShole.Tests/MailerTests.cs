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
        #region "Mail Test Settings"

        readonly string hostName = "mail.creativemode.co.za";
        readonly string portNumber = "2079";
        readonly string userName = "accounts@creativemode.co.za";
        readonly string password = "Pr9)J}@@C#}7";
        readonly string toEmail = "ljshole@gmail.com";
        readonly string ccEmail = "ljshole@yahoo.com";
        readonly string bccEmail = "hloni@creativemode.co.za";
        readonly bool sslRequired = false;

        #endregion "Mail Test Settings"

        [Fact]
        public void Can_Send_Mail_With_String_Body_With_No_Attachment()
        {
            var emailIsSent = new FluentEmail()
                                    .UsingSMTPServer(hostName, portNumber, userName, password, sslRequired)
                                    .Message("Fluent Email Subject : No Attachments", new List<MailAddress> { new (toEmail) }, new (userName, "Fluent Email"))
                                        .SetBodyEncoding(Encoding.UTF8)
                                        .FromMailAddresses(new MailAddress(userName, "Testing Fluent Emailer"))
                                    .Body("This is me Testing")
                                    .Send();

            Assert.True(emailIsSent);
        }

        [Fact]
        public void Can_Send_Mail_With_String_Body_With_Attachment()
        {
            var emailIsSent = new FluentEmail()
                                    .UsingSMTPServer(hostName, portNumber, userName, password, sslRequired)
                                    .Message("Fluent Email - With Attachments", new List<MailAddress> { new(toEmail) }, new(userName, "Fluent Email"))
                                        .SetBodyEncoding(Encoding.UTF8)
                                        .FromMailAddresses(new MailAddress(userName, "Testing Fluent Emailer"))
                                        .AddAttachments(new List<Attachment> { new Attachment($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SamplePDF.pdf")}") })
                                    .Body("This is me Testing")
                                    .Send();

            Assert.True(emailIsSent);
        }

        [Fact]
        public void Can_Send_Mail_With_String_Body_Add_BccAndCcEmails()
        {
            var emailIsSent = new FluentEmail()
                                    .UsingSMTPServer(hostName, portNumber, userName, password, sslRequired)
                                    .Message("Fluent Email - With Attachments - Bcc and CC Emails", new List<MailAddress> { new(toEmail) }, new(userName, "Fluent Email"))
                                        .SetBodyEncoding(Encoding.UTF8)
                                        .FromMailAddresses(new MailAddress(userName, "Testing Fluent Emailer"))
                                        .CcMailAddresses(new List<MailAddress> { new MailAddress(ccEmail) })
                                        .CcMailAddresses(new List<MailAddress> { new MailAddress(bccEmail) })
                                        .AddAttachments(new List<Attachment> { new Attachment($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SamplePDF.pdf")}") })
                                    .Body("This is me Testing")
                                    .Send();

            Assert.True(emailIsSent);
        }

        [Fact]
        public void Can_Send_Mail_Using_Email_Template_With_No_Attachments()
        {
            var emailIsSent = new FluentEmail()
                                    .UsingSMTPServer(hostName, portNumber, userName, password, sslRequired)
                                    .Message("Fluent Email - No Attachments - Bcc and CC Emails", new List<MailAddress> { new(toEmail) }, new(userName, "Fluent Email"))
                                        .SetBodyEncoding(Encoding.UTF8)
                                        .FromMailAddresses(new MailAddress(userName, "Testing Fluent Emailer"))
                                        .CcMailAddresses(new List<MailAddress> { new MailAddress(ccEmail) })
                                        .CcMailAddresses(new List<MailAddress> { new MailAddress(bccEmail) })
                                    .Body("This is me Testing")
                                    .Send();

            Assert.True(emailIsSent);
            //var emailIsSent = new Mailer()
            //                .SetUpMessage()
            //                     .Subject("Fluent Email Subject : Template - No Attachments")
            //                     .FromMailAddresses(new MailAddress(userName, "Fluent Email - Template - No Attachments"))
            //                     .ToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
            //                     .SetUpBody()
            //                        .SetBodyEncoding(Encoding.UTF8)
            //                        .SetBodyTransferEncoding(TransferEncoding.Unknown)
            //                        .Body()
            //                             .UsingEmailTemplate($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestHtmlPage.html")}")
            //                             .UsingTemplateDictionary(new Dictionary<string, string> { { "{{subject}}", "Testing Message" }, { "{{body}}", "<section><h2>This is</h2><p>Welcome to our world</p></section>" } })
            //                             .CompileTemplate()
            //                             .SetBodyIsHtmlFlag()
            //               .SetPriority(MailPriority.Normal)
            //               .WithCredentials()
            //                    .UsingHostServer(hostName)
            //                    .OnPortNumber(portNumber)
            //                    .WithUserName(userName)
            //                    .WithPassword(password)
            //                .Send();

            //Assert.True(emailIsSent);
        }

        [Fact]
        public void Can_Send_Mail_Using_Email_Template_With_Attachments()
        {
            //var emailIsSent = new Mailer()
            //                .SetUpMessage()
            //                     .Subject("Fluent Email Subject : Template - With Attachments")
            //                     .FromMailAddresses(new MailAddress(userName, "Fluent Email - Template - With Attachments"))
            //                     .ToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
            //                     .WithTheseAttachments(new List<Attachment> { new Attachment($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SamplePDF.pdf")}") })
            //                     .SetUpBody()
            //                        .SetBodyEncoding(Encoding.UTF8)
            //                        .SetBodyTransferEncoding(TransferEncoding.Unknown)
            //                        .Body()
            //                             .UsingEmailTemplate($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestHtmlPage.html")}")
            //                             .UsingTemplateDictionary(new Dictionary<string, string> { { "{{subject}}", "Testing Message" }, { "{{body}}", "<section><h2>This is</h2><p>Welcome to our world</p></section>" } })
            //                             .CompileTemplate()
            //                             .SetBodyIsHtmlFlag()
            //               .SetPriority(MailPriority.Normal)
            //               .WithCredentials()
            //                    .UsingHostServer(hostName)
            //                    .OnPortNumber(portNumber)
            //                    .WithUserName(userName)
            //                    .WithPassword(password)
            //                .Send();

            //Assert.True(emailIsSent);
        }

        [Fact]
        public void Can_Send_Mail_Using_Email_Template_With_TemplateValues_Overload_With_Attachments()
        {
            //var emailIsSent = new Mailer()
            //                .SetUpMessage()
            //                     .Subject("Fluent Email : Template With TemplateValues Overload")
            //                     .FromMailAddresses(new MailAddress(userName, "Fluent Email - Template With TemplateValues Overload - With Attachments"))
            //                     .ToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
            //                     .WithTheseAttachments(new List<Attachment> { new Attachment($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SamplePDF.pdf")}") })
            //                     .SetUpBody()
            //                        .SetBodyEncoding(Encoding.UTF8)
            //                        .SetBodyTransferEncoding(TransferEncoding.Unknown)
            //                        .Body()
            //                             .UsingEmailTemplate($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestHtmlPage.html")}",
            //                                new Dictionary<string, string> {
            //                                    { "{{subject}}", "Testing Message" },
            //                                    { "{{body}}", "<section><h2>Template Values Overload</h2><p>Working Beautifully!</p></section>" }
            //                                })
            //                             .CompileTemplate()
            //                             .SetBodyIsHtmlFlag()
            //               .SetPriority(MailPriority.Normal)
            //               .WithCredentials()
            //                    .UsingHostServer(hostName)
            //                    .OnPortNumber(portNumber)
            //                    .WithUserName(userName)
            //                    .WithPassword(password)
            //                .Send();

            //Assert.True(emailIsSent);
        }

        [Fact]
        public void Can_Send_Mail_With_ReplyTo_SubjectEncoding_BodyEncoding_BodyTranser_Set()
        {
            //var emailIsSent = new Mailer()
            //        .SetUpMessage()
            //            .Subject("Fluent Email Subject : No Attachments - With ReplyTo")
            //            .FromMailAddresses(new MailAddress(userName, "Fluent Email - No Attachments - With ReplyTo"))
            //            .ToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
            //            .SubjectEncoding(Encoding.UTF8)
            //            .ReplyTo(new MailAddress("info@creativemode.co.za"))
            //            .SetUpBody()
            //                .SetBodyEncoding(Encoding.UTF8)
            //                .SetBodyTransferEncoding(TransferEncoding.Unknown)
            //                .Body()
            //                    .UsingString("This is me Testing")
            //                    .SetBodyIsHtmlFlag()
            //       .SetPriority(MailPriority.Normal)
            //       .WithCredentials()
            //            .UsingHostServer(hostName)
            //            .OnPortNumber(portNumber)
            //            .WithUserName(userName)
            //            .WithPassword(password)
            //        .Send();

            //Assert.True(emailIsSent);
        }

        [Fact]
        public void Can_Send_Mail_Using_Injected_Credentials()
        {
            //var emailIsSent = new Mailer(new MailCredentials { PortNumber = portNumber, HostServer = hostName, Password = password, UserName = userName })
            //        .SetUpMessage()
            //            .Subject("Fluent Email Subject : Injected  Credentials")
            //            .FromMailAddresses(new MailAddress(userName, "Fluent Email - Injected  Credentials"))
            //            .ToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
            //            .SubjectEncoding(Encoding.UTF8)
            //            .ReplyTo(new MailAddress("info@creativemode.co.za"))
            //            .SetUpBody()
            //                .SetBodyEncoding(Encoding.UTF8)
            //                .SetBodyTransferEncoding(TransferEncoding.Unknown)
            //                .Body()
            //                    .UsingString("This is me Testing")
            //                    .SetBodyIsHtmlFlag()
            //       .SetPriority(MailPriority.Normal)
            //       .UsingTheInjectedCredentials()
            //       .Send();

            //Assert.True(emailIsSent);
        }

        [Fact]
        public void Throws_ArgumentNullException_When_Email_Subject_Is_Not_Provided()
        {
            var response = Assert.Throws<ArgumentNullException>(() => new FluentEmail()
                                    .UsingSMTPServer(hostName, portNumber, userName, password, sslRequired)
                                    .Message(string.Empty, new List<MailAddress> { new (toEmail) }, new (userName, "Fluent Email - No Attachments"))
                                        .SetBodyEncoding(Encoding.UTF8)
                                        .SetBodyTransferEncoding(TransferEncoding.Unknown)
                                        .FromMailAddresses(new MailAddress(userName, "Testing Fluent Emailer"))
                                    .Body("This is me Testing")
                                    .Send());

            Assert.True(response.Message.Equals($"Value cannot be null. (Parameter 'subject')"));
        }

        [Fact]
        public void Throws_ArgumentNullException_When_To_Email_Is_Not_Provided()
        {
            var response = Assert.Throws<ArgumentNullException>(() => new FluentEmail()
                                    .UsingSMTPServer(hostName, portNumber, userName, password, sslRequired)
                                    .Message("Test Subject", null, new MailAddress(userName, "Fluent Email - No Attachments"))
                                        .SetBodyEncoding(Encoding.UTF8)
                                        .SetBodyTransferEncoding(TransferEncoding.Unknown)
                                        .FromMailAddresses(new MailAddress(userName, "Testing Fluent Emailer"))
                                    .Body("This is me Testing")
                                    .Send());

            Assert.True(response.Message.Equals($"Value cannot be null. (Parameter 'toEmailAddresses')"));
        }

        [Fact]
        public void Throws_ArgumentNullException_When_Cc_Email_Is_Not_Provided()
        {
            var response = Assert.Throws<ArgumentNullException>(() => new FluentEmail()
                                      .UsingSMTPServer(hostName, portNumber, userName, password, sslRequired)
                                      .Message("Test Subject", new List<MailAddress> { new (toEmail) }, new(userName, "Fluent Email - No Attachments"))
                                          .SetBodyEncoding(Encoding.UTF8)
                                          .SetBodyTransferEncoding(TransferEncoding.Unknown)
                                          .FromMailAddresses(new (userName, "Testing Fluent Emailer"))
                                          .CcMailAddresses(null)
                                      .Body("This is me Testing")
                                      .Send());

            Assert.True(response.Message.Equals($"Value cannot be null. (Parameter 'ccMailAddresses')"));
        }

        [Fact]
        public void Throws_ArgumentNullException_When_Bcc_Email_Is_Not_Provided()
        {
            var response = Assert.Throws<ArgumentNullException>(() => new FluentEmail()
                           .UsingSMTPServer(hostName, portNumber, userName, password, sslRequired)
                           .Message("Test Subject", new List<MailAddress> { new (toEmail) }, new (userName, "Fluent Email - No Attachments"))
                               .SetBodyEncoding(Encoding.UTF8)
                               .SetBodyTransferEncoding(TransferEncoding.Unknown)
                               .FromMailAddresses(new (userName, "Testing Fluent Emailer"))
                               .BccMailAddresses(null)
                           .Body("This is me Testing")
                           .Send());

            Assert.True(response.Message.Equals($"Value cannot be null. (Parameter 'bccMailAddresses')"));
        }

        [Fact]
        public void Throws_ArgumentNullException_When_From_Email_Is_Not_Provided()
        {
            var response = Assert.Throws<ArgumentNullException>(() => new FluentEmail()
                            .UsingSMTPServer(hostName, portNumber, userName, password, sslRequired)
                            .Message("Test Subject", new List<MailAddress> { new MailAddress(toEmail) }, new MailAddress(userName, "Fluent Email - No Attachments"))
                                .SetBodyEncoding(Encoding.UTF8)
                                .SetBodyTransferEncoding(TransferEncoding.Unknown)
                                .FromMailAddresses(null)
                            .Body("This is me Testing")
                            .Send());

            Assert.True(response.Message.Equals($"Value cannot be null. (Parameter 'fromMailAddress')"));
        }
    }
}
