using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BuyEasySellEasy.Startup))]
namespace BuyEasySellEasy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
