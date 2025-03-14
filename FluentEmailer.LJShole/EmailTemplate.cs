using FluentEmailer.LJShole.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FluentEmailer.LJShole
{
    /// <summary>
    /// Class definition for using a template file. This class should not be used directly.
    /// </summary>
    public class EmailTemplate : IEmailTemplate
    {
        //private readonly IMailer _mailer;
        //private readonly IEmailMessage _emailMessage;
        //private string _emailBody;
        //private IEmailBodySetter _emailBodyInstance;
        //private string _templateLocation;
        //private Dictionary<string, string> _templateValues;

        //public EmailTemplate(IEmailMessage emailMessage, IMailer mailer, IEmailBodySetter emailBody)
        //{
        //    _emailMessage = emailMessage;
        //    _mailer = mailer;
        //    _emailBodyInstance = emailBody;
        //}

        ///// <summary>
        ///// The body of the email will use this template file. 
        ///// </summary>
        ///// <param name="templateLocation">Location of the template file to use as the body of the email. NB This does not support URI's.</param>
        ///// <returns></returns>
        //public IEmailTemplate UsingEmailTemplate(string templateLocation)
        //{
        //    if (string.IsNullOrEmpty(templateLocation))
        //        throw new ArgumentNullException(nameof(templateLocation));

        //    _templateLocation = templateLocation;
        //    return this;
        //}

        ///// <summary>
        ///// Dictionary containing key/value pairs. Keys in this dictionary represent placeholders found on the template file specified / to be specified.
        ///// </summary>
        ///// <param name="templateValues"></param>
        ///// <returns></returns>
        //public IEmailTemplate UsingTemplateDictionary(Dictionary<string, string> templateValues)
        //{
        //    if (templateValues == null || templateValues.Keys.Count == 0)
        //        throw new InvalidOperationException("Template values are required.");

        //    _templateValues = templateValues;
        //    return this;
        //}

        ///// <summary>
        ///// The body of the email will use this template file. 
        ///// </summary>
        ///// <param name="templateLocation">Location of the template file to use as the body of the email. NB This does not support URI's.</param>
        ///// <param name="templateValues"></param>
        ///// <returns></returns>
        //public IEmailTemplate UsingEmailTemplate(string templateLocation, Dictionary<string, string> templateValues)
        //{
        //    if (string.IsNullOrEmpty(templateLocation))
        //        throw new ArgumentNullException(nameof(templateLocation));

        //    if (templateValues == null || templateValues.Keys.Count == 0)
        //        throw new InvalidOperationException("Template values are required.");

        //    _templateLocation = templateLocation;
        //    _templateValues = templateValues;
        //    return this;
        //}

        ///// <summary>
        ///// Sets the body of the email to a this string.
        ///// </summary>
        ///// <param name="emailBody"></param>
        ///// <returns></returns>
        //public IEmailBodySetter UsingString(string emailBody)
        //{
        //    if (string.IsNullOrEmpty(emailBody))
        //        throw new ArgumentNullException(nameof(emailBody));

        //    _emailBody = emailBody;

        //   ( _emailMessage as EmailMessage).SetBody(_emailBody);
        //    _emailBodyInstance = _emailBodyInstance ?? new EmailBodySetter(_emailMessage, _mailer);
        //    return _emailBodyInstance;
        //}
        ///// <summary>
        ///// Replaces the placeholders in the template file with the dictionary specified in the 'UsingTemplateDictionary' method.
        ///// </summary>
        ///// <returns></returns>
        //public IEmailBodySetter CompileTemplate()
        //{
        //    if (string.IsNullOrEmpty(_emailBody))
        //    {
        //        using (var reader = new StreamReader(_templateLocation))
        //        {
        //            _emailBody = reader.ReadToEnd();
        //            _templateValues.Keys.ToList().ForEach(key =>
        //            {
        //                _emailBody = _emailBody.Replace(key, _templateValues[key]);
        //            });
        //        }
        //    }

        //   ( _emailMessage as EmailMessage).SetBody(_emailBody);
            
        //    _emailBodyInstance = _emailBodyInstance ?? new EmailBodySetter(_emailMessage, _mailer);

        //    return _emailBodyInstance;
        //}

    }
}
