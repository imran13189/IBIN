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
    public class ChemicalAPIController : ApiController
    {
        [HttpPost]
        public dynamic GetChemical([FromBody]DataTableRequest<Chemical> model)
        {
            try
            {
                ChemicalRepository _repo = new ChemicalRepository();
                model.data = _repo.GetChemical(model.search.value);
                var data = model.data.OrderBy(x => x.ChemicalId).Skip(model.start).Take(model.length);
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