using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ZmW_FinancialPortal.Startup))]
namespace ZmW_FinancialPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
