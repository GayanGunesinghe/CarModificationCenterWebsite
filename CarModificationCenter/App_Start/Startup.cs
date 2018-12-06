/*
 * Startup.cs (Middleware) is the pipeline between the
 * communication of the server and the application.
 */

using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace CarModificationCenter.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()   //cookie based authentication.
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/auth/login")   //redirect to url if user not autheticated.
            });
        }
    }
}