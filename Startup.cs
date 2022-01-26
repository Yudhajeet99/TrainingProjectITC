using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCAppointment.Startup))]
namespace MVCAppointment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
