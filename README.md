# FluentEmailer
Fluent API to send emails in a .NET Core application using your own SMTP settings

# Usage (Simple string as body of the email)
        new Mailer()
          .Message()
            .WithSubject("Mail Subject")
            .AddFromMailAddresses(new MailAddress(userName, "Fluent Email"))
            .AddToMailAddresses(new List<MailAddress> { new MailAddress(toEmail) })
            .WithBody()
                .UsingString("Email body as string")
            .SetBodyIsHtmlFlag()
          .WithCredentials()
            .UsingHostServer(hostName)
            .OnPortNumber(portNumber)
            .WithUserName(userName)
            .WithPassword(password)
          .Send();
***hostName*** The SMPT/IMAP server you'll be using to send emails from. <br/>
***portNumber*** The port number used by the ***hostName***<br/>
***userName*** The email address you'll be using to send emails from as configured on ***hostName**. <br/>
***password*** The password used to log in when accessing the email address (***userName***)<br/>

# Usage (Providing a template file)
      new Mailer()
        .Message()
            .WithSubject("Mail Subject Template")
            .AddFromMailAddresses(new MailAddress(userName,"Fluent Email"))
            .AddToMailAddresses(new List<MailAddress> { new MailAddress(***toEmail***) })
            .AddCcMailAddresses(new List<MailAddress> { new MailAddress(***ccEmail***) })
            .AddBccMailAddresses(new List<MailAddress> { new MailAddress(***bccEmail***) })
            .WithBody()
                .UsingEmailTemplate($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"TestHtmlPage.html")}")
                .UsingTemplateDictionary(new Dictionary<string, string> { { "{{subject}}","Testing Message"}, { "{{body}}", "<section><h2>This is</h2><p>Welcome to our world</p></section>" } })
                .CompileTemplate()
            .SetBodyIsHtmlFlag()
       .WithCredentials()
            .UsingHostServer(hostName)
            .OnPortNumber(portNumber)
            .WithUserName(userName)
            .WithPassword(password)
        .Send();
        
 > This usage requires a ***Dictionary<string,string>*** where the keys in the dictionary represent placeholders in the template to be replaced by their respective values.<br/>
 
 # Usage (Adding Attachments)
        new Mailer()
                .Message()
                .WithSubject("Mail Subject Template")
                .AddFromMailAddresses(new MailAddress(userName,"Fluent Email"))
                .AddToMailAddresses(new List<MailAddress> { new MailAddress(***toEmail***) })
                .AddCcMailAddresses(new List<MailAddress> { new MailAddress(***ccEmail***) })
                .AddBccMailAddresses(new List<MailAddress> { new MailAddress(***bccEmail***) })
                .WithAttachments(new List<Attachment> { new Attachment($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SamplePDF.pdf")}") })
                .WithBody()
                        .UsingEmailTemplate($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"TestHtmlPage.html")}")
                        .UsingTemplateDictionary(new Dictionary<string, string> { { "{{subject}}","Testing Message"}, { "{{body}}", "<section><h2>This is</h2><p>Welcome to our                                 world</p></section>" } })
                        .CompileTemplate()
                .SetBodyIsHtmlFlag()
                .WithCredentials()
                        .UsingHostServer(hostName)
                        .OnPortNumber(portNumber)
                        .WithUserName(userName)
                        .WithPassword(password)
                .Send();
