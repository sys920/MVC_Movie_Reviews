using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_EX1_MovieReviews.Startup))]
namespace MVC_EX1_MovieReviews
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
