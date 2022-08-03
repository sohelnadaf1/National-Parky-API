using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static NationalParkApi.Models.Trial;

namespace NationalParkApi.Models.Dtos
{
    public class TrailsUpdateDtos
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Distance { get; set; }

        public DiffcultyType Difficult { get; set; }

        [Required]
        public int NationalParkId { get; set; }

      
    }
}
