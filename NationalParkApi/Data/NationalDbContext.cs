using Microsoft.EntityFrameworkCore;
using NationalParkApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalParkApi.Data
{
    public class NationalDbContext:DbContext
    {
        public NationalDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<NationalPark> nationalParks { get; set; }
        public DbSet<Trial> trials { get; set; }
    }
}
