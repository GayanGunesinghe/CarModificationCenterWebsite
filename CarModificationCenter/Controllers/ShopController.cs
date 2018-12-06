using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using CarModificationCenter.Models;

namespace CarModificationCenter.Controllers
{
    [AllowAnonymous]    
    public class ShopController : Controller
    {
        private MainDbContext db = new MainDbContext();

        // GET: Shop
        public ActionResult ShopNow(int page = 1, int pageSize = 20)
        {
            try
            {
                List<website_vehicles> listVehicles = db.Vehicles.ToList();
                PagedList<website_vehicles> model = new PagedList<website_vehicles>(listVehicles, page, pageSize);
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // get the vehicle picture from database
        public FileContentResult LoadPicture(int itemId)
        {
            try
            {
                var getPath = db.Vehicles.Where(u => u.Id == itemId).Select(u => u.Picture01);
                var materializePath = getPath.ToList();
                byte[] path = materializePath[0];

                if (path != null)
                {
                    return new FileContentResult(path, "image/png");
                }

                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        //// search shop items
        //public ActionResult ShopSearch(string query, string category, int page = 1, int pageSize = 10)
        //{
        //    List<website_vehicles> listVehicles = db.Vehicles.ToList();
        //    List<website_vehicles> newVehicles = db.Vehicles.ToList();
        //    foreach (var item in listVehicles)
        //    {
        //        if (category == "Manufacturer" && item.Manufacturer != query)
        //        {
        //            newVehicles.Remove(item);
        //        }
        //    }

        //    PagedList<website_vehicles> model = new PagedList<website_vehicles>(newVehicles, page, pageSize);
        //    return View(model);
        //}
    }
}