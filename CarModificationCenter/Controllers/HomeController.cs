using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Linq;
using System.Web.Mvc;
using CarModificationCenter.Models;
using PagedList;

namespace CarModificationCenter.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: Home
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            List<website_vehicles> listVehicles = db.Vehicles.ToList();
            List<website_vehicles> newVehicles = db.Vehicles.ToList();
            foreach (var item in listVehicles)
            {
                if (((DateTime.Now - Convert.ToDateTime(item.Timestamp)).TotalDays) > 10)
                {
                    newVehicles.Remove(item);
                }
            }

            PagedList<website_vehicles> model = new PagedList<website_vehicles>(newVehicles, page, pageSize);
            return View(model);
        }

        //display the profile picture of logged in user.
        public FileContentResult DisplayPicture()
        {
            var db = new MainDbContext();

            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    //render image from database and display.
                    var getPath = db.Users.Where(u => u.FirstName + " " + u.LastName == User.Identity.Name)
                        .Select(u => u.DisplayPicture);
                    var materializePath = getPath.ToList();
                    byte[] path = materializePath[0];

                    return new FileContentResult(path, "image/png");
                }
                catch (ArgumentOutOfRangeException)
                {
                    var getPath = db.Users.Where(u => u.FirstName == "System")
                        .Select(u => u.DisplayPicture);
                    var materializePath = getPath.ToList();
                    var path = materializePath[0];

                    return new FileContentResult(path, "image/png");
                }
                catch (EntityException)
                {
                    return null;
                }
                catch (DataException)
                {
                    return null;
                }
                catch (TimeoutException)
                {
                    return null;
                }
            }
            return null;
        }
    }
}