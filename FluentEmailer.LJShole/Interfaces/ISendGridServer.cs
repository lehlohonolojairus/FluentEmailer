namespace FluentEmailer.LJShole.Interfaces
{
    public interface ISendGridServer
    {
        IEmailMessage UsingTemplate(string templateId);
    }
}