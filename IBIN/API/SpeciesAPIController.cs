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
        public dynamic GetSpecies([FromBody]DataTableRequest model)
        {
            try
            {
                SpeciesRepository _repo = new SpeciesRepository();
                model.data = _repo.GetSpecies();
                var data = model.data.OrderBy(x => x.SpeciesId).Skip(model.start).Take(model.length);
                return Json(new
                {
                    // this is what datatables wants sending back
                    draw = model.draw,
                    recordsTotal = model.data.Count,
                    recordsFiltered = model.data.Count,
                    data = data,
                    length=model.length
                });
              
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        public IHttpActionResult GetName()
        {

            string v;
            return null;
        }
    }
}