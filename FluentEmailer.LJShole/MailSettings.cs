using System;

namespace FluentEmailer.LJShole
{
    [Obsolete("This class is not in use, will be in use in future releases of the package.")]
    public class MailSettings //: IMailSettings
    {
        public string UserName { get ; set ; }
        public string Password { get ; set ; }
        public int PortNumber { get ; set ; }
        public string CompanyName { get ; set ; }
    }

}
