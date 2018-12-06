using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarModificationCenter.Models;

namespace CarModificationCenter.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Checkout()
        {
            return View();
        }

        // GET: Payment
        public ActionResult Payment()
        {
            return View();
        }
    }
}