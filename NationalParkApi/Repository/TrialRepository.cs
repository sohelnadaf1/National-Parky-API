
using Microsoft.EntityFrameworkCore;
using NationalParkApi.Data;
using NationalParkApi.Models;
using NationalParkApi.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParkApi.Repository
{
    public class TrialRepository : ITrailRepository
    {
        private readonly NationalDbContext _db;

        public TrialRepository(NationalDbContext db)
        {
            _db = db;
        }

        public ICollection<Trial> GetTrials()
        {
           return _db.trials.Include(c => c.NationalPark).OrderBy(a => a.Name).ToList();
            
        }


        public Trial GetTrailById(int id)
        {
          var data =  _db.trials.Include(c => c.NationalPark).FirstOrDefault(d => d.Id == id);
            return data;    
        }

        public ICollection<Trial> GetTrialNationalPark(int nationalpid)
        {
            var data = _db.trials.Include(c => c.NationalPark).Where(c => c.NationalParkId == nationalpid).ToList();
            return data;
        }

        public bool CreateTrail(Trial trial)
        {
            _db.trials.Add(trial);
            return Save();
        }


        public bool TrailExists(string name)
        {
            bool value= _db.trials.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool TrailExists(int id)
        {
            bool value = _db.trials.Any(a => a.Id == id);
            return value;
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateTrailPark(Trial trial)
        {
            _db.trials.Update(trial);
            return Save();
                 
        }

        public bool DeleteTrail(Trial trial)
        {
            _db.trials.Remove(trial);
            return Save();
        }
    }
}
