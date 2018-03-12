using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBIN.BLL;
using IBIN.DAL;
using IBIN.CORE.Models;

namespace IBIN.Controllers
{
    public class SpeciesController : Controller
    {
        // GET: Species
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult SpeciesList(DataTableRequest<Species> model)
        {
            try
            {
                SpeciesRepository _repo = new SpeciesRepository();
                ViewBag.PageCount = (model.recordsTotal % model.length)>0? (model.recordsTotal / model.length)+1 : (model.recordsTotal / model.length);
                ViewBag.CurrentPageIndex = model.draw;
                return PartialView("_SpeciesList", model);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public ActionResult Description(int Id)
        {
            return View(new SpeciesRepository().GetSpeciesDetail(Id));
        }
    }
}