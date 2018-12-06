using System;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using CarModificationCenter.CustomLibraries;
using CarModificationCenter.Models;
using MySql.Data.MySqlClient;

namespace CarModificationCenter.Controllers
{
    public class UserAccountController : Controller
    {
        // GET: UserAccount
        public ActionResult ManageAccount(website_users model)
        {
            try
            {
                var db = new MainDbContext();

                //get the user account details from the database.
                var getEmail = db.Users.Where(u => u.FirstName + " " + u.LastName == User.Identity.Name)
                    .Select(u => u.Email);
                var materializeEmail = getEmail.ToList();
                var email = materializeEmail[0];

                var getMobile = db.Users.Where(u => u.FirstName + " " + u.LastName == User.Identity.Name)
                    .Select(u => u.Mobile);
                var materializeMobile = getMobile.ToList();
                var mobile = materializeMobile[0];

                var getCountry = db.Users.Where(u => u.FirstName + " " + u.LastName == User.Identity.Name)
                    .Select(u => u.Country);
                var materializeCountry = getCountry.ToList();
                var country = materializeCountry[0];

                var getAddress = db.Users.Where(u => u.FirstName + " " + u.LastName == User.Identity.Name)
                    .Select(u => u.Address);
                var materializeAddress = getAddress.ToList();
                var address = materializeAddress[0];

                //assign values to model.
                model.Email = email;
                model.Mobile = mobile;
                model.Country = country;
                model.Address = address;

                if (email == null || mobile == 0 || country == null || address == null)
                {
                    TempData["msg-type"] = "alert-info";
                    TempData["msg-sub-head"] = "Heads up!!";
                    TempData["msg-des"] = "Please fill in the missing user information to complete your profile!";
                }

                return View(model);
            }
            catch (EntityException)
            {
                TempData["msg-type"] = "alert-danger";
                TempData["msg-head"] = "Oh Snap!!";
                TempData["msg-des"] = "Could not load user details!! Please Check you connection!";
                return View(model);
            }
            catch (MySqlException)
            {
                TempData["msg-type"] = "alert-danger";
                TempData["msg-head"] = "Oh Snap!!";
                TempData["msg-des"] = "Could not load user details!! Please Check you connection!";
                return View(model);
            }
            catch (ArgumentOutOfRangeException)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult UpdateDetails(UserDetailsViewModel model)
        {
            var db = new MainDbContext();

            var getId = db.Users.Where(u => u.FirstName + " " + u.LastName == User.Identity.Name).Select(u => u.Id);
 
            var materializeId = getId.ToList();
            var id = materializeId[0];

            var nUser = db.Users.Single(u => u.Id == id);
            nUser.Country = model.Country;
            nUser.Address = model.Address;
            nUser.Mobile = model.Mobile;
            db.SaveChanges();

            return RedirectToAction("ManageAccount", "UserAccount");
        }

        //[HttpPost]
        //public ActionResult ChangePassword(UserDetailsViewModel model)
        //{
        //    var db = new MainDbContext();

        //    var getId = db.Users.Where(u => u.FirstName + " " + u.LastName == User.Identity.Name).Select(u => u.Id);
        //    var materializeId = getId.ToList();
        //    var id = materializeId[0];

        //    var getPassword = db.Users.Where(u => u.FirstName + " " + u.LastName == User.Identity.Name).Select(u => u.Password);
        //    var materializePassword = getPassword.ToList();
        //    var password = materializePassword[0];

        //    var decryptedPassword = CustomDecrypt.Decrypt(password);
        //    if (decryptedPassword == model.Password)
        //    {
        //        if (model.NewPassword == model.ConfirmPassword)
        //        {
        //            var encryptedPassword = CustomEncrypt.Encrypt(model.Password);
                    
        //            var nUser = db.Users.Single(u => u.Id == id);
        //            nUser.Password = encryptedPassword;
        //        }
        //        else
        //        {

        //        }
        //    }
        //    else
        //    {

        //    }

        //    db.SaveChanges();

        //    return RedirectToAction("ManageAccount", "UserAccount");
        //}
    }
}
