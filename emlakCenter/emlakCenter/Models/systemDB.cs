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
        public DbSet<Medya> medyalar { get; set; }
        public DbSet<il> iller { get; set; }
        public DbSet<ilce> ilceler { get; set; }
        public DbSet<semt> semtler { get; set; }
        public DbSet<arsa> arsalar { get; set; }
        public DbSet<ilan> ilanlar { get; set; }
        public DbSet<tapuDurumu> tapuDurumlari { get; set; }
        public DbSet<ilanSahibi> ilanSahipleri { get; set; }
        public DbSet<admin> adminler { get; set; }
    }
}