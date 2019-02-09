using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(hospital.Startup))]
namespace hospital
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
 
