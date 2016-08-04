using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaheyHealth.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Start Page?";

            return View();
        }

        public ActionResult AdminHome()
        {
            //Check if the user is logged in and redirect to login if he isn't
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            else { 
                ViewBag.Message = "Administration page";
                return View();
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Description Message?";

            return View();
        }
    }
}