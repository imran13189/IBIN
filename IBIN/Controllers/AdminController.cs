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
using System.Security.Principal;

namespace IBIN.Controllers
{
    [Authenticated]
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
            TempData["catergoryType"] = "Species";
            return RedirectToAction("Index");
        }


        [Authenticated]
        public ActionResult Chemical()
        {
            return View();
        }
        [Authenticated]
        public ActionResult AddChemical(int id = 0)
        {
            if (id > 0)
            {
                return View(new SpeciesRepository().GetSpeciesDetail(id));
            }
            return View();
        }

        [Authenticated]
        [HttpPost]
        public ActionResult AddChemical(Chemical model, HttpPostedFileBase SpeciesFile)
        {
            ChemicalRepository _repo = new ChemicalRepository();
            model = _repo.AddChemical(model);
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
                    nFileName = "PC_" + model.ChemicalId + extension;
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
                    _repo.AddChemical(model);
                }

            }
            TempData["catergoryType"] = "Chemical";
            return RedirectToAction("Index");
        }

        

        [HttpGet]
        public ActionResult ChangePassword()
        {
          
            return View();

        }

        [HttpPost]
        public ActionResult ChangePassword(string OldPassword,string NewPassword)
        {
            UserRepository repo = new UserRepository();
            if(repo.ChangePassword(OldPassword, NewPassword)==null)
            {
                ViewBag.Message = "Old password is wrong";
            }
            else
            {
                ViewBag.Message = "Password has been changed";
            }
            return View();

        }


        public ActionResult LogOut()
        {
            //Session.Abandon();
            FormsAuthentication.SignOut();
            //System.Web.HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
                
            return RedirectToAction("Index", "Admin");
        }

      
        public ActionResult IsActiveSpecies(int id)
        {
            SpeciesRepository _repo = new SpeciesRepository();
            _repo.IsActive(id);
            return Json(true);
        }

        public ActionResult IsActiveChemical(int id)
        {
            ChemicalRepository _repo = new ChemicalRepository();
            _repo.IsActive(id);
            return Json(true);
        }

    }
}