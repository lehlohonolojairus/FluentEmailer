using System;
using System.Collections.Generic;
using System.Text;

namespace FluentEmailer.LJShole.Interfaces
{
    public interface IMailCredentialSettings
    {
        string UserName { get; set; }
        string Password { get; set; }
        string PortNumber { get; set; }
        string HostServer { get; set; }
    }
}
