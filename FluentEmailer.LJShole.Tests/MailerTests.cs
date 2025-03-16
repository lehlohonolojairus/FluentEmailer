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

        readonly string hostName = string.Empty;
        readonly string portNumber = string.Empty;
        readonly string userName = string.Empty;
        readonly string password = string.Empty;
        readonly string toEmail = string.Empty;
        readonly string ccEmail = string.Empty;
        readonly string bccEmail = string.Empty;
        readonly string replyToEmail = string.Empty;
        readonly bool sslRequired = true;

        #endregion "Mail Test Settings"

        [Fact]
        public void Can_Send_Mail_With_String_Body_With_No_Attachment()
        {
            var emailIsSent = new FluentEmail()
                                    .UsingSMTPServer(hostName, portNumber, userName, password, sslRequired)
                                    .Message("Fluent Email Subject : No Attachments", new List<MailAddress> { new(toEmail) }, new(userName, "Fluent Email"))
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
                                        .AddAttachments([new($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SamplePDF.pdf")}")])
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
                                        .CcMailAddresses([new MailAddress(ccEmail)])
                                        .BccMailAddresses([new MailAddress(bccEmail)])
                                        .AddAttachments([new($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SamplePDF.pdf")}")])
                                    .Body("This is me Testing")
                                    .Send();
            Assert.True(emailIsSent);
        }

        [Fact]
        public void Can_Send_Mail_Using_Email_Template_With_TemplateValues_No_Attachments()
        {
            var templateValues = new Dictionary<string, string> { { "{{subject}}", "Testing Message" }, { "{{body}}", "<section><h2>This is</h2><p>Welcome to our world</p></section>" } };

            var emailIsSent = new FluentEmail()
                                    .UsingSMTPServer(hostName, portNumber, userName, password, sslRequired)
                                    .Message("Fluent Email - Template File - No Attachments", new List<MailAddress> { new(toEmail) }, new(userName, "Fluent Email"))
                                        .CcMailAddresses([new MailAddress(ccEmail)])
                                        .BccMailAddresses([new MailAddress(bccEmail)])
                                    .Body(templateValues, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestHtmlPage.html"))
                                    .Send();

            Assert.True(emailIsSent);
        }

        [Fact]
        public void Can_Send_Mail_Using_Email_Template_With_TemplateValues_With_Attachments()
        {
            var templateValues = new Dictionary<string, string> { { "{{subject}}", "Testing Message" }, { "{{body}}", "<section><h2>This is</h2><p>Welcome to our world</p></section>" } };

            var emailIsSent = new FluentEmail()
                                    .UsingSMTPServer(hostName, portNumber, userName, password, sslRequired)
                                    .Message("Fluent Email - Template File - With Attachments", new List<MailAddress> { new(toEmail) }, new(userName, "Fluent Email"))
                                        .CcMailAddresses([new MailAddress(ccEmail)])
                                        .BccMailAddresses([new MailAddress(bccEmail)])
                                        .AddAttachments([new($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SamplePDF.pdf")}")])
                                    .Body(templateValues, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestHtmlPage.html"))
                                    .Send();

            Assert.True(emailIsSent);
        }

        [Fact]
        public void Can_Send_Mail_With_ReplyTo_SubjectEncoding_BodyEncoding_BodyTranser_Set()
        {
            var emailIsSent = new FluentEmail()
                                    .UsingSMTPServer(hostName, portNumber, userName, password, sslRequired)
                                    .Message("Fluent Email - With Attachments - Bcc and CC Emails", new List<MailAddress> { new(toEmail) }, new(userName, "Fluent Email"))
                                        .CcMailAddresses([new MailAddress(ccEmail)])
                                        .BccMailAddresses([new MailAddress(bccEmail)])
                                        .AddAttachments([new($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SamplePDF.pdf")}")])
                                        .ReplyTo(new MailAddress(replyToEmail))
                                        .SubjectEncoding(Encoding.UTF8)
                                        .BodyEncoding(Encoding.UTF8)
                                    .Body("This is me Testing")
                                    .Send();

            Assert.True(emailIsSent);
        }

        [Fact]
        public void Throws_ArgumentNullException_When_Email_Subject_Is_Not_Provided()
        {
            var response = Assert.Throws<ArgumentNullException>(() => new FluentEmail()
                                    .UsingSMTPServer(hostName, portNumber, userName, password, sslRequired)
                                    .Message(string.Empty, new List<MailAddress> { new(toEmail) }, new(userName, "Fluent Email - No Attachments"))
                                        .BodyTransferEncoding(TransferEncoding.Unknown)
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
                                        .BodyTransferEncoding(TransferEncoding.Unknown)
                                    .Body("This is me Testing")
                                    .Send());

            Assert.True(response.Message.Equals($"Value cannot be null. (Parameter 'toEmailAddresses')"));
        }

        [Fact]
        public void Throws_ArgumentNullException_When_Cc_Email_Is_Not_Provided()
        {
            var response = Assert.Throws<ArgumentNullException>(() => new FluentEmail()
                                      .UsingSMTPServer(hostName, portNumber, userName, password, sslRequired)
                                      .Message("Test Subject", new List<MailAddress> { new(toEmail) }, new(userName, "Fluent Email"))
                                          .BodyTransferEncoding(TransferEncoding.Unknown)
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
                           .Message("Test Subject", new List<MailAddress> { new(toEmail) }, new(userName, "Fluent Email"))
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
                            .Message("Test Subject", new List<MailAddress> { new(toEmail) }, new(userName, "Fluent Email"))
                                .FromMailAddress(null)
                            .Body("This is me Testing")
                            .Send());

            Assert.True(response.Message.Equals($"Value cannot be null. (Parameter 'fromMailAddress')"));
        }
    }
}
