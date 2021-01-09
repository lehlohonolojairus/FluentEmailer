﻿using System.Collections.Generic;

namespace FluentEmailer.LJShole.Interfaces
{
    public interface IEmailTemplate
    {
        IEmailMessage CompileTemplate();
        IEmailTemplate UsingEmailTemplate(string templateLocation);
        IEmailMessage UsingString(string emailBody);
        IEmailTemplate UsingTemplateDictionary(Dictionary<string, string> templateValues);
    }
}