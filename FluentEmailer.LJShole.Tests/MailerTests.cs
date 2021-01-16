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
        readonly string hostName = string.Empty;
        readonly string portNumber = string.Empty;
        readonly string userName = string.Empty;
        readonly string password = string.Empty;
        readonly string toEmail = string.Empty;
        readonly string ccEmail = string.Empty;
        readonly string bccEmail = string.Empty;

        [Fact]
        public void Can_Send_Mail_With_String_Body_With_No_Attachment()
        {
            var emailIsSent = new Mailer()
                    .SetUpMessage()
                        .WithSubject("Mail Subject")
                        .AddFromMailAddresses(new MailAddress(userName, "Fluent Email - No Attachments"))
                        .AddToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
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
                                    .WithSubject("Mail Subject")
                                    .AddFromMailAddresses(new MailAddress(userName, "Fluent Email - With Attachments"))
                                    .AddToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
                                    .WithAttachments(new List<Attachment> { new Attachment($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SamplePDF.pdf")}") })
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
                                   .WithSubject("Mail Subject")
                                   .AddFromMailAddresses(new MailAddress(userName, "Fluent Email - No Attachments - Bcc and CC Emails"))
                                   .AddToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
                                   .AddCcMailAddresses(new List<MailAddress> { new MailAddress(ccEmail) })
                                   .AddBccMailAddresses(new List<MailAddress> { new MailAddress(bccEmail) })
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
                                 .WithSubject("Mail Subject Template")
                                 .AddFromMailAddresses(new MailAddress(userName, "Fluent Email - Template - No Attachments"))
                                 .AddToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
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
                                 .WithSubject("Mail Subject Template")
                                 .AddFromMailAddresses(new MailAddress(userName, "Fluent Email - Template - With Attachments"))
                                 .AddToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
                                 .WithAttachments(new List<Attachment> { new Attachment($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SamplePDF.pdf")}") })
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
                        .WithSubject(string.Empty)
                        .AddFromMailAddresses(new MailAddress(userName, "Fluent Email - With Attachments"))
                        .AddToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
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
                        .WithSubject("Test Subject")
                        .AddFromMailAddresses(new MailAddress(userName, "Fluent Email - With Attachments"))
                        .AddToMailAddresses(null)
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
                        .WithSubject("Test Subject")
                        .AddFromMailAddresses(new MailAddress(userName, "Fluent Email - With Attachments"))
                        .AddToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
                        .AddCcMailAddresses(null)
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
                        .WithSubject("Test Subject")
                        .AddFromMailAddresses(new MailAddress(userName, "Fluent Email - With Attachments"))
                        .AddToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
                        .AddBccMailAddresses(null)
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
                        .WithSubject("Test Subject")
                        .AddFromMailAddresses(null)
                        .AddToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
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
                        .WithSubject("Mail Subject")
                        .AddFromMailAddresses(new MailAddress(userName, "Fluent Email - No Attachments - With ReplyTo"))
                        .AddToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
                        .SetSubjectEncoding(Encoding.UTF8)
                        .WithReplyTo(new MailAddress("info@creativemode.co.za"))
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
    }
}
