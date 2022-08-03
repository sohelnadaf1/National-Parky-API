using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParkApi.Models
{
    public class Trial
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Distance { get; set; }
        public enum DiffcultyType { Easy,Medium,Difficult,Expert}
        public DiffcultyType Difficult { get; set; }

        [Required]
        public int NationalParkId { get; set; }
        [ForeignKey("NationalParkId")]
        public NationalPark NationalPark { get; set; }

        public DateTime DateCreated { get; set; }

    }
}
