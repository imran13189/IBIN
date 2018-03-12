using IBIN.CORE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBIN.BLL;
using IBIN.DAL;
namespace IBIN.Controllers
{
    public class ChemicalController : Controller
    {
        // GET: Chemical
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult ChemicalList(DataTableRequest<Chemical> model)
        {
            try
            {
                ChemicalRepository _repo = new ChemicalRepository();
                ViewBag.PageCount = (model.recordsTotal % model.length) > 0 ? (model.recordsTotal / model.length) + 1 : (model.recordsTotal / model.length);
                ViewBag.CurrentPageIndex = model.draw;
                return PartialView("_ChemicalList", model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public ActionResult Description(int Id)
        {
            return View(new ChemicalRepository().GetChemicalDetails(Id));
        }
    }
}