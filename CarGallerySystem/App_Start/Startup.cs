using System;
using System.Threading.Tasks;
using CarGallerySystem.Data;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(CarGallerySystem.App_Start.Startup))]

namespace CarGallerySystem.App_Start
{
    public class Startup
    {
        static Startup()
        {
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(15),
            };
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<ApplicationDbContext>(() => new ApplicationDbContext());

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",

                LoginPath = new PathString("/Account/Login")
            });
        }
    }
}
