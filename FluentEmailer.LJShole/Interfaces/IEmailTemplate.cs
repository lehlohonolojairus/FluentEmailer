using System.Collections.Generic;

namespace FluentEmailer.LJShole.Interfaces
{
    public interface IEmailTemplate
    {
        IEmailBody CompileTemplate();
        IEmailTemplate UsingEmailTemplate(string templateLocation);
        IEmailBody UsingString(string emailBody);
        IEmailTemplate UsingTemplateDictionary(Dictionary<string, string> templateValues);
    }
}