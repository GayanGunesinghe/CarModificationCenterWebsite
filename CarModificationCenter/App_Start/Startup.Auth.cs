/*
 * Startup.Auth.cs (Middleware) is the pipeline between the
 * communication of the server and the application.
 */

using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;

namespace CarModificationCenter
{
    public partial class Startup
    {
        private void ConfigureAuth(IAppBuilder app)
        {
            var cookieOptions = new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/auth/login")   //redirect to url if user not autheticated.
            };

            app.UseCookieAuthentication(cookieOptions);     //cookie based authentication.

            app.SetDefaultSignInAsAuthenticationType(cookieOptions.AuthenticationType); //set default auth type

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "905607939224-rdgtspokul2fbtoqbpd2lra15gr9f13t.apps.googleusercontent.com",
                ClientSecret = "D8zfn7qMDf-5JIXbHrGvvEN9",
                CallbackPath = new PathString("/GoogleLoginCallback")
            });
        }
    }
}