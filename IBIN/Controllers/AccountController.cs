using IBIN.BLL;
using IBIN.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace IBIN.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            UserRepository _repo = new UserRepository();
            User model = _repo.Login(UserName, Password);
            if (model != null)
            {
                FormsAuthentication.SetAuthCookie(UserName, false);
                return RedirectToAction("Index", "Admin");
            }

            return RedirectToAction("Index");
        }
    }
}