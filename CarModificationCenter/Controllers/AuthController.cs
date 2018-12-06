using System.Data.Entity.Core;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using CarModificationCenter.Models;
using CarModificationCenter.CustomLibraries;
using Microsoft.Owin.Security;

namespace CarModificationCenter.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        // GET: Auth
        [HttpGet]   //get value of url query string.
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)    //check if input field are empty.
            {
                TempData["msg-type"] = "alert-warning";
                TempData["msg-head"] = "Warning";
                TempData["msg-des"] = "Email/Password cannot be empty!!";
                return View(model);
            }

            try
            {
                using (var db = new MainDbContext())
                {
                    var getPassword = db.Users.Where(u => u.Email == model.Email).Select(u => u.Password); //get password linked to the given email
                    var materializePassword = getPassword.ToList();

                    if (materializePassword.Count == 0) //count = 0 if the email is not available.
                    {
                        TempData["msg-type"] = "alert-warning";
                        TempData["msg-head"] = "Warning";
                        TempData["msg-des"] = "Account does not exist!!";
                        return View(model);
                    }

                    var password = materializePassword[0];
                    var decryptedPassword = CustomDecrypt.Decrypt(password);

                    if (model.Email != null && model.Password == decryptedPassword
                    ) //check if email password combination is valid
                    {
                        var getFirstName = db.Users.Where(u => u.Email == model.Email).Select(u => u.FirstName);
                        var materializeFirstName = getFirstName.ToList();
                        var firstname = materializeFirstName[0];

                        var getLastName = db.Users.Where(u => u.Email == model.Email).Select(u => u.LastName);
                        var materializeLastName = getLastName.ToList();
                        var lastname = materializeLastName[0];

                        var getEmail = db.Users.Where(u => u.Email == model.Email).Select(u => u.Email);
                        var materializeEmail = getEmail.ToList();
                        var email = materializeEmail[0];

                        var getCountry = db.Users.Where(u => u.Email == model.Email).Select(u => u.Country);
                        var materializeCountry = getCountry.ToList();
                        var country = materializeCountry[0];

                        var identify = new ClaimsIdentity(new[] //creation of cookie.
                        {
                            new Claim(ClaimTypes.Name, firstname + " " + lastname),
                            new Claim(ClaimTypes.Email, email),
                            new Claim(ClaimTypes.Country, country),
                        }, "ApplicationCookie");

                        var ctx = Request.GetOwinContext();
                        var authManager = ctx.Authentication;

                        authManager.SignIn(identify);

                        return RedirectToAction("Index", "Home"); //redirect to homepage
                    }
                    TempData["msg-type"] = "alert-warning";
                    TempData["msg-head"] = "Warning";
                    TempData["msg-des"] = "Invalid Email or Password!!";
                    return View(model);
                }
            }
            catch (ProviderIncompatibleException)
            {
                TempData["msg-type"] = "alert-danger";
                TempData["msg-head"] = "Oh Snap!!";
                TempData["msg-des"] = "Unable to login!! Could not establish connection with server!!";
                return View(model);
            }
            catch (EntityException)
            {
                TempData["msg-type"] = "alert-danger";
                TempData["msg-head"] = "Oh Snap!!";
                TempData["msg-des"] = "Server Timeout!! Check your connection!!";
                return View(model);
            }
        }


        //social login implementation
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            return new RedirectResult(returnUrl);
        }

        public ActionResult SocialLogin(string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult("Google",
                Url.Action("ExternalLoginCallback", "Auth", new { ReturnUrl = returnUrl }));
        }

        // Implementation copied from a standard MVC Project, with some stuff
        // that relates to linking a new external login to an existing identity
        // account removed.
        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }



        //------------------------------------------------------------------------------



        public ActionResult Logout()    //clear cookies and redirect to homepage.
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(RegistrationViewModel model, HttpPostedFileBase file1)
        {
            if (ModelState.IsValid)     //check if fields empty/valid
            {
                try
                {
                    using (var db = new MainDbContext())
                    {
                        var emailCheck = db.Users.FirstOrDefault(u => u.Email == model.Email);
                        if (emailCheck == null) //check if account with same email exsist.
                        {
                            var encryptedPassword = CustomEncrypt.Encrypt(model.Password);
                            var user = db.Users.Create();

                            user.Email = model.Email;
                            user.Password = encryptedPassword;
                            user.FirstName = model.FirstName;
                            user.LastName = model.LastName;
                            user.Country = RegionInfo.CurrentRegion.DisplayName;

                            //save user uploaded picture to the database as binary.
                            byte[] imgByte = null;
                            if (file1 != null && file1.ContentLength > 0)
                            {
                                MemoryStream target = new MemoryStream();
                                file1.InputStream.CopyTo(target);
                                imgByte = target.ToArray();
                                user.DisplayPicture = imgByte;
                            }
                            else
                            {
                                user.DisplayPicture = imgByte; //set null
                            }

                            db.Users.Add(user);     //add provided data to sample database
                            db.SaveChanges();

                            TempData["msg-type"] = "alert-success";
                            TempData["msg-head"] = "Success";
                            TempData["msg-des"] = "Account created successfully!! Login from here.";
                            return RedirectToAction("Login", "Auth");
                        }
                        TempData["msg-type"] = "alert-warning";
                        TempData["msg-head"] = "Warning";
                        TempData["msg-des"] = "Account already exist for this email!!";
                        return View();
                    }
                }
                catch (ProviderIncompatibleException)
                {
                    TempData["msg-type"] = "alert-danger";
                    TempData["msg-head"] = "Oh Snap!!";
                    TempData["msg-des"] = "Unable to create account!! Could not establish connection with server!!";
                    return View(model);
                }
            }
            TempData["msg-type"] = "alert-warning";
            TempData["msg-head"] = "Warning";
            TempData["msg-des"] = "Account creation was unsuccessful. Please correct the errors and try again!!";
            return View();
        }
    }
}