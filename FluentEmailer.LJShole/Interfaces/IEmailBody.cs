using System.Net.Mime;
using System.Text;

namespace FluentEmailer.LJShole.Interfaces
{
    public interface IEmailBody
    {
        IEmailTemplate Body();
        IEmailBody SetBodyTransferEncoding(TransferEncoding transferEncoding);
        IEmailBody SetBodyEncoding(Encoding encoding);
        IEmailMessage SetBodyIsHtmlFlag(bool emailBodyIsHtml = true);
    }
}