using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace emlakCenter.Models
{
    public class arsa
    {
        public int Id { get; set; }
        //KONUM
        public int il { get; set; }
        public int ilce { get; set; }
        public int semt { get; set; }

        public int metrekare { get; set; }

        public int fiyat { get; set; }

        public int tapuDurumu { get; set; }

        

        public bool hasResim { get; set; }
        public bool hasHarita { get; set; }
        public bool hasVideo { get; set; }

        
    }

    public class ilan
    {
        public int id { get; set; }

        public string ilanNo { get; set; }
        public DateTime ilantarihi { get; set; }
        public int ilanSahibi { get; set; }
        public int ilanTuru { get; set; } // 1 => arsa
        public bool takasDurum { get; set; } // 1=> takas yapılabilir
       
    }
    public class il
    {
        public int Id { get; set; }
        public string il_adi { get; set; }
    }

    public class ilce
    {
        public int Id { get; set; }
        public int il_id { get; set; }
        public string ilce_adi { get; set; }
    }

    public class semt
    {
        public int Id { get; set; }
        public int ilce_id { get; set; }
        public string semt_adi { get; set; }
    }

    public class tapuDurumu
    {
        public int Id { get; set; }
        public string durum { get; set; }
    }

    public class ilanSahibi
    {
        public int Id { get; set; }
        public string ilanSahibi_adi { get; set; }
    }
}