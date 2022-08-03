using AutoMapper;
using NationalParkApi.Models;
using NationalParkApi.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParkApi.ParkyMapper
{
    public class ParkyMappling : Profile
    {
        public ParkyMappling()
        {
            CreateMap<NationalPark, NationalParkDtos>().ReverseMap();
            CreateMap<Trial,TrialDtos>().ReverseMap();
            CreateMap<Trial, TrailsCreateDtos>().ReverseMap();
            CreateMap<Trial,TrailsUpdateDtos>().ReverseMap();
        }
    }
}
