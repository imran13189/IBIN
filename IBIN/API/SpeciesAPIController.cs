using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using IBIN.DAL;
using IBIN.CORE.Models;
using IBIN.BLL;

namespace IBIN.API
{
    public class SpeciesAPIController : ApiController
    {
        [HttpPost]
        public dynamic GetSpecies(DataTableRequest model)
        {
            
            SpeciesRepository _repo = new SpeciesRepository();
            model.dataList = _repo.GetSpecies(model);
            var data = model.dataList.OrderBy(x => x.SpeciesId).Skip(model.start).Take(model.length);
            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = model.dataList.Count,
                recordsFiltered = model.dataList.Count,
                data = data
            });

        }

        public IHttpActionResult GetName()
        {

            string v;
            return null;
        }
    }
}