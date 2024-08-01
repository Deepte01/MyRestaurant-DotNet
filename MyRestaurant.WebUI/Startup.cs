using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyRestaurant.WebUI.Startup))]
namespace MyRestaurant.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
