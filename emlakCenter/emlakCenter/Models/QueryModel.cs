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
        public String semtID { get; set; }

        public String metrekareStart { get; set; }
        public String metrekareEnd { get; set; }

        public String fiyatStart { get; set; }
        public String fiyatEnd { get; set; }

        public bool resim { get; set; }
        public bool video { get; set; }
        public bool harita { get; set; }

        public String tarih { get; set; }

        public String keyword { get; set; }

    }
}