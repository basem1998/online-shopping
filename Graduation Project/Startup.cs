using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Graduation_Project.Startup))]
namespace Graduation_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
