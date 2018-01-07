using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBIN.DAL;
using IBIN.BLL;

namespace IBIN.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Species()
        {
            return View();
        }

        public ActionResult AddSpecies(int id=0)
        {
            if(id>0)
            {
                return View(new SpeciesRepository().GetSpeciesDetail(id));
            }
            return View();
        }
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
                        if (dfile.FullName == nFileName)
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


    }
}