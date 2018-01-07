using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBIN.DAL;
using IBIN.BLL;
using System.Web.Security;
using IBIN.Filters;

namespace IBIN.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        [Authenticated]
        public ActionResult Species()
        {
            return View();
        }
        [Authenticated]
        public ActionResult AddSpecies(int id=0)
        {
            if(id>0)
            {
                return View(new SpeciesRepository().GetSpeciesDetail(id));
            }
            return View();
        }

        [Authenticated]
        [HttpPost]
        public ActionResult AddSpecies(Species model, HttpPostedFileBase SpeciesFile)
        {
            SpeciesRepository _repo = new SpeciesRepository();
            model=_repo.AddSpecies(model);
            String nFileName = "";//, nLicenseCopy = "", nInsuranceCopy = "", nOtherAttachment = "";
            if ((SpeciesFile != null) && (SpeciesFile.ContentLength > 0))
            {
                if (!(Directory.Exists(Server.MapPath("~/UploadedFiles/SpeciesFile"))))
                {
                    Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/SpeciesFile"));
                }

                if ((SpeciesFile != null) && (SpeciesFile.ContentLength > 0))
                {
                    string extension = Path.GetExtension(SpeciesFile.FileName);
                    nFileName = "PC_" + model.SpeciesId + extension;
                    var DirectoryPath = Server.MapPath("~/UploadedFiles/SpeciesFile");
                    var path = Path.Combine(DirectoryPath, System.IO.Path.GetFileName(nFileName));

                    DirectoryInfo dInfo = new DirectoryInfo(DirectoryPath);

                    foreach (FileInfo dfile in dInfo.GetFiles())
                    {
                        if (dfile.Name == nFileName)
                        {
                            dfile.Delete();
                            break;
                        }

                    }
                    SpeciesFile.SaveAs(path);
                    model.FileName = nFileName;
                    _repo.AddSpecies(model);
                }

            }
            return RedirectToAction("Species");
        }

        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            UserRepository _repo = new UserRepository();
            User model= _repo.Login(UserName, Password);
            if(model!=null)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
               1, // Ticket version
               UserName, // Username associated with ticket
               DateTime.Now, // Date/time issued
               DateTime.Now.AddMinutes(60), // Date/time to expire
               true, // "true" for a persistent user cookie
               UserName // User-data, in this case the roles
              );

                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                Session["TokenID"] = ticket;
                return RedirectToAction("Species", "Admin", new { Area = "Admin" });
            }

            return View();
        }

        

    }
}