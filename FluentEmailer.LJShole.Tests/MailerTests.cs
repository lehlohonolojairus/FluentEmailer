using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
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
        public void Can_Send_Mail_With_String_Body()
        {
            var emailIsSent = new Mailer()
                    .Message()
                        .WithSubject("Mail Subject")
                        .AddFromMailAddresses(new MailAddress(userName, "Fluent Email"))
                        .AddToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
                        .AddCcMailAddresses(new List<MailAddress> { new MailAddress(ccEmail) })
                        .AddBccMailAddresses(new List<MailAddress> { new MailAddress(bccEmail) })
                        .WithAttachments(new List<Attachment> { new Attachment($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"SamplePDF.pdf")}") })
                        .WithBody()
                            .UsingString("Email body as string")
                            .BuildMessageBody()
                        .SetBodyIsHtmlFlag()
                   .WithCredentials()
                        .UsingHostServer(hostName)
                        .OnPortNumber(portNumber)
                        .WithUserName(userName)
                        .WithPassword(password)
                    .Send();

            Assert.True(emailIsSent);
        }

        [Fact]
        public void Can_Send_Mail_Using_Email_Template()
        {
            var emailIsSent = new Mailer()
                    .Message()
                        .WithSubject("Mail Subject Template")
                        .AddFromMailAddresses(new MailAddress(userName, "Fluent Email"))
                        .AddToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
                        .AddCcMailAddresses(new List<MailAddress> { new MailAddress(ccEmail) })
                        .AddBccMailAddresses(new List<MailAddress> { new MailAddress(bccEmail) })
                        .WithBody()
                            .UsingEmailTemplate($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestHtmlPage.html")}")
                            .UsingTemplateDictionary(new Dictionary<string, string> { { "{{subject}}", "Testing Message" }, { "{{body}}", "<section><h2>This is</h2><p>Welcome to our world</p></section>" } })
                            .BuildMessageBody()
                        .SetBodyIsHtmlFlag()
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
