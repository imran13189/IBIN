using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IBIN.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string search=" ")
        {
            string chemical = "chemical";
            string species = "species";
            if (chemical.Contains(search.ToLower()))
            {
                return RedirectToAction("Index", "Chemical");
            }
            else if (species.Contains(search.ToLower()))
            {
                return RedirectToAction("Index", "Species");
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}