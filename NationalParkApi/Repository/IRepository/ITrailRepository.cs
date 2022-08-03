using NationalParkApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParkApi.Repository.IRepository
{
   public interface ITrailRepository
    {
        ICollection<Trial> GetTrials();
        Trial GetTrailById(int id);
        ICollection<Trial> GetTrialNationalPark(int nationalpid);
        bool TrailExists(string name);
        bool TrailExists(int id);
        bool CreateTrail(Trial trial);
        bool UpdateTrailPark(Trial trial);
        bool DeleteTrail(Trial trial);
        bool Save();
    }
}
