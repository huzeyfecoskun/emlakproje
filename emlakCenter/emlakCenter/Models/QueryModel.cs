using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace emlakCenter.Models
{
    public class QueryModel
    {
        public int il { get; set; }
        public int ilce { get; set; }
        public int semt { get; set; }
        
        public int metrekareStart { get; set; }
        public int metrekareEnd { get; set; }

        public int fiyatStart { get; set; }
        public int fiyatEnd { get; set; }

        public bool resim { get; set; }
        public bool video { get; set; }
        public bool harita { get; set; }

        public String tarih { get; set; }
        public int tapuDurum { get; set; }
        public String keyword { get; set; }

    }
}