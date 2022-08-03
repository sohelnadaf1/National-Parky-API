using NationalParkApi.Data;
using NationalParkApi.Models;
using NationalParkApi.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParkApi.Repository
{
    public class NationalParkRepository : INationalParkRepository
    {

        private readonly NationalDbContext _db;

        public NationalParkRepository(NationalDbContext db)
        {
            _db = db;
        }
        public bool CreateNationlPark(NationalPark nationalPark)
        {
            _db.nationalParks.Add(nationalPark);
            return Save();
        }

        public bool DeleteNationlPark(NationalPark nationalPark)
        {
            _db.nationalParks.Remove(nationalPark);
            return Save();
        }

        public ICollection<NationalPark> GetAllNAtionalPark()
        {
            var data = _db.nationalParks.OrderBy(a => a.Name).ToList();
            return data;
        }

        public NationalPark GetNationalParkById(int id)
        {
            var data = _db.nationalParks.FirstOrDefault(NID => NID.Id == id);
            return data;
        }

        public bool NationalParkExists(string name)
        {
            var data = _db.nationalParks.Any(a => a.Name.ToLower().Trim() == name.ToLower().Trim());
            return data;
            
        }

        public bool NationalParkEXists(int id)
        {
            var data = _db.nationalParks.Any(a => a.Id == id);
            return data;
                
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationlPark(NationalPark nationalPark)
        {
            _db.nationalParks.Update(nationalPark);
            return Save();
        }
    }
}
