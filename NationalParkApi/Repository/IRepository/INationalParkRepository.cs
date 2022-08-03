using NationalParkApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParkApi.Repository.IRepository
{
   public interface INationalParkRepository
    {
        ICollection<NationalPark> GetAllNAtionalPark();
        NationalPark GetNationalParkById(int id);
        bool NationalParkExists(string name);
        bool NationalParkEXists(int id);
        bool CreateNationlPark(NationalPark nationalPark);
        bool UpdateNationlPark(NationalPark nationalPark);
        bool DeleteNationlPark(NationalPark nationalPark);
        bool Save();

    }
}
