using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using GDHOTE.Hub.WebApi.OwinProvider;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(GDHOTE.Hub.WebApi.Startup))]

namespace GDHOTE.Hub.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //HttpConfiguration config = new HttpConfiguration();
            //WebApiConfig.Register(config);
            //app.UseWebApi(config);

            HttpConfiguration config = new HttpConfiguration();
            ConfigureOAuth(app);
            WebApiConfig.Register(config);

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);

            var logPath = AppDomain.CurrentDomain.BaseDirectory + @"\log4net.config";
            log4net.Config.XmlConfigurator.Configure(new FileInfo(logPath));
        }



        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/v1/auth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(5),
                Provider = new CustomOwinProvider(),
                RefreshTokenProvider = new RefreshTokenProvider()
            };
            // Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}