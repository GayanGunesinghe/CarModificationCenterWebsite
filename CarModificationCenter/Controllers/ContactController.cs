using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using CarModificationCenter.Models;

namespace CarModificationCenter.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(contact cntct)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    MailMessage msz = new MailMessage();
                    msz.From = new MailAddress(cntct.Email);//Email which you are getting 
                    //from contact us page 
                    msz.To.Add("gayangunesinghe@gmail.com");//Where mail will be sent 
                    msz.Subject = cntct.Subject;
                    msz.Body = cntct.Message;
                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = "smtp.gmail.com";

                    smtp.Port = 587;

                    smtp.Credentials = new System.Net.NetworkCredential
                        ("gayangunesinghe@gmail.com", "Anuja1995");

                    smtp.EnableSsl = true;

                    smtp.Send(msz);

                    ModelState.Clear();

                    TempData["msg-type"] = "alert-success";
                    TempData["msg-head"] = "Success!!";
                    TempData["msg-des"] = "Thank you for contacting us!!";
                }
                catch (Exception ex)
                {
                    ModelState.Clear();
                    ViewBag.Message = $" Sorry we are facing Problem here {ex.Message}";
                }
            }

            TempData["msg-type"] = "alert-warning";
            TempData["msg-head"] = "Empty Fields!!";
            TempData["msg-des"] = "Please fill in the empty fields and try again!!";

            return View();
        }
    }
}