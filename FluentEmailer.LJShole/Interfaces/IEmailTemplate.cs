using System.Collections.Generic;

namespace FluentEmailer.LJShole.Interfaces
{
    public interface IEmailTemplate
    {
        IEmailBodySetter CompileTemplate();
        IEmailTemplate UsingEmailTemplate(string templateLocation);
        IEmailBodySetter UsingString(string emailBody);
        IEmailTemplate UsingTemplateDictionary(Dictionary<string, string> templateValues);
    }
}