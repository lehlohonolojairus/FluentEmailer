# FluentEmailer
Fluent API to send emails in a .NET Core application using your own SMTP settings

<a href="https://www.buymeacoffee.com/hloni" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-blue.png" alt="Buy Me A Coffee" height="41" width="174"></a>

## V 1.1.1 Changes

This change addresses an issue with setting the `Bcc` and `Cc` email addresses; these were added onto the `To` email list.

This change also includes a change to the `.FromMailAddresses` method; since an email can only originate from one email address, this method should be named accordingly i.e in singular form not plural.

#### Usage (using string as body of email)
```
_fluentEmail
    .UsingSMTPServer(hostName, portNumber, userName, password, sslRequired)
    .Message("Email Subject", new List<MailAddress> { new(toEmail) }, new(userName, "Email Display Name"))
    .FromMailAddress(new MailAddress(***userName***))             
    .Body("Test body content")
    .Send();
```
---
## V 1.1.0 Changes

Future versions of the application / package will provide support for SendGrid, with this in  mind, I have introduced breaking changes which pave way for the coming support. In addition, the change aims to make using the package much more intuitive and seemless. We do still support dependency injection. Simply add sample code below in your program.cs and import the `FluentEmailer.LJShole` namespace in the program.cs file.

`builder.Services.AddFluentEmailer()` 

This will allow you to inject the `IFluentEmail` interface which you can use your code.
#### Usage (using string as body of email)
        _fluentEmail
          .UsingSMTPServer(hostName, portNumber, userName, password, sslRequired)
          .Message("Email Subject", new List<MailAddress> { new(toEmail) }, new(userName, "Email Display Name"))
              .FromMailAddresses(new MailAddress(***userName***))
              .CcMailAddresses([new MailAddress(***ccEmail***)])
              .BccMailAddresses([new MailAddress(***bccEmail***)])
              .AddAttachments([new($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SamplePDF.pdf")}")])
          .Body("Test body content")
          .Send();

#### Usage (providing a template file for the body of the email)
         var templateValues = new Dictionary<string, string> { { "{{subject}}", "Testing Message" }, { "{{body}}", "<section><h2>This is</h2><p>Welcome to our world</p></section>" } };

         _fluentEmail
          .UsingSMTPServer(hostName, portNumber, userName, password, sslRequired)
          .Message("Email Subject", new List<MailAddress> { new(toEmail) }, new(userName, "Email Display Name"))
              .FromMailAddresses(new MailAddress(***userName***))
              .CcMailAddresses([new MailAddress(***ccEmail***)])
              .BccMailAddresses([new MailAddress(***bccEmail***)])
              .AddAttachments([new($"{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SamplePDF.pdf")}")])
          .Body(templateValues, Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestHtmlPage.html"))
          .Send();

---
## V 1.0.4
#### Usage (Simple string as body of the email)
        new Mailer()
          .Message()
            .WithSubject("Email Subject")
            .AddFromMailAddresses(new MailAddress(userName))
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

#### Usage (Providing a template file)
      new Mailer()
        .Message()
            .WithSubject("Email Subject")
            .AddFromMailAddresses(new MailAddress(userName))
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
 
 #### Usage (Adding Attachments)
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
                
                
## V 1.0.3 Changes

        new Mailer()
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

