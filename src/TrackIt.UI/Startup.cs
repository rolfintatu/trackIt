using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrackIt.UI.Startup))]
namespace TrackIt.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
