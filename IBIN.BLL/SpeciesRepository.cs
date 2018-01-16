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
            if (model.SpeciesId == 0)
            {
                _db.Species.Add(model);
                
            }
            else
            {
                Species species = _db.Species.Find(model.SpeciesId);
                species.SpeciesName = model.SpeciesName;
                species.FileName = model.FileName;
            }
            _db.SaveChanges();
            return model;
        }
        public Species DeleteSpecies(Species model)
        {
            _db.Species.Remove(model);
            return model;
        }
        public List<Species> GetSpecies(string search)
        {
            try
            {
                List<Species> data = _db.Species.ToList(); ;
                if (!string.IsNullOrEmpty(search))
                {
                  data = _db.Species.Where(x => x.SpeciesName.Contains(search)).ToList();
                }
                return data;
            }
            catch(Exception e)
            {
                throw e;
            }
        }


        public Species GetSpeciesDetail(int SpeciesId)
        {
            return _db.Species.FirstOrDefault(x => x.SpeciesId == SpeciesId);
        }
    }
}
