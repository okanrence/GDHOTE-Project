using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GDHOTE.Hub.Mvc.Startup))]
namespace GDHOTE.Hub.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
