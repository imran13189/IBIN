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

        public ActionResult AddSpecies()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSpecies(Species model, HttpPostedFileBase SpeciesFile)
        {
            SpeciesRepository _repo = new SpeciesRepository();
            _repo.AddSpecies(model);
            String nFileName = "";//, nLicenseCopy = "", nInsuranceCopy = "", nOtherAttachment = "";
            if ((SpeciesFile != null) && (SpeciesFile.ContentLength > 0))
            {
                if (!(Directory.Exists(Server.MapPath("~/UploadedFiles/Species/" + model.SpeciesId + "/SpeciesFile"))))
                {
                    Directory.CreateDirectory(Server.MapPath("~/UploadedFiles/Species/" + model.SpeciesId + "/SpeciesFile"));
                }

                if ((SpeciesFile != null) && (SpeciesFile.ContentLength > 0))
                {
                    string extension = Path.GetExtension(SpeciesFile.FileName);
                    nFileName = "PC_" + model.SpeciesId + extension;
                    var DirectoryPath = Server.MapPath("~/UploadedFiles/Customer/" + model.SpeciesId + "/PassportCopy");
                    var path = Path.Combine(DirectoryPath, System.IO.Path.GetFileName(nFileName));

                    DirectoryInfo dInfo = new DirectoryInfo(DirectoryPath);
                    foreach (FileInfo dfile in dInfo.GetFiles())
                    { dfile.Delete(); }
                    SpeciesFile.SaveAs(path);
                    model.FileName = nFileName;
                    //res = this._car.EditCar(objCar);
                }

            }
            return View();
        }


    }
}