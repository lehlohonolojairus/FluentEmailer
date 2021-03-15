using System.Net.Mime;
using System.Text;

namespace FluentEmailer.LJShole.Interfaces
{
    public interface IEmailBodySetter
    {
        IEmailTemplate Body();
        IEmailBodySetter SetBodyTransferEncoding(TransferEncoding transferEncoding);
        IEmailBodySetter SetBodyEncoding(Encoding encoding);
        IEmailMessage SetBodyIsHtmlFlag(bool emailBodyIsHtml = true);
    }
}