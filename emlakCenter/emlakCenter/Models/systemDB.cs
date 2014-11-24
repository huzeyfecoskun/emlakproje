using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace emlakCenter.Models
{
    public class systemDB : DbContext
    {
        //public systemDB()
        //    : base("")
        //{

        //}

        public DbSet<arsaMedya> ArsaMedya { get; set; }

    }
}