using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace emlakCenter.Models
{
    public class systemDB : DbContext
    {
        public systemDB()
            : base("name=DefaultConnection")
        {
        }

        public DbSet<arsaMedya> ArsaMedya { get; set; }
        public DbSet<il> iller { get; set; }
        public DbSet<ilce> ilceler { get; set; }
    }
}