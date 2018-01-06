using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBIN.DAL;
using IBIN.CORE.Models;
namespace IBIN.BLL
{
    public class SpeciesRepository:Connection
    {
        public Species AddSpecies(Species model)
        {
            _db.Species.Add(model);
            _db.SaveChanges();
            return model;
        }
        public Species DeleteSpecies(Species model)
        {
            _db.Species.Remove(model);
            return model;
        }
        public List<Species> GetSpecies(DataTableRequest request)
        {
            return _db.Species.ToList();
        }
    }
}
