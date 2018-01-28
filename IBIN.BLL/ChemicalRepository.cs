using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBIN.DAL;
using IBIN.CORE.Models;
namespace IBIN.BLL
{
    public class ChemicalRepository : Connection
    {
        public Chemical AddChemical(Chemical model)
        {
            if (model.ChemicalId == 0)
            {
                _db.Chemicals.Add(model);
                
            }
            else
            {
                Species species = _db.Species.Find(model.ChemicalId);
                species.SpeciesName = model.ChemicalName;
                species.FileName = model.FileName;
            }
            _db.SaveChanges();
            return model;
        }
        public Chemical DeleteChemical(Chemical model)
        {
            _db.Chemicals.Remove(model);
            return model;
        }
        public List<Chemical> GetChemical(string search)
        {
            try
            {
                List<Chemical> data = _db.Chemicals.ToList(); ;
                if (!string.IsNullOrEmpty(search))
                {
                  data = _db.Chemicals.Where(x => x.ChemicalName.Contains(search)).ToList();
                }
                return data;
            }
            catch(Exception e)
            {
                throw e;
            }
        }


        public Chemical GetChemicalDetails(int SpeciesId)
        {
            return _db.Chemicals.FirstOrDefault(x => x.ChemicalId == SpeciesId);
        }

        public Chemical IsActive(int id)
        {
            var data = _db.Chemicals.FirstOrDefault(x => x.ChemicalId == id);
            data.IsActive = data.IsActive ? false : true;
            _db.SaveChanges();
            return data;
        }
    }
}
