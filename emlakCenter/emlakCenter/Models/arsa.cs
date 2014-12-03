using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace emlakCenter.Models
{
    public class arsa
    {
        public int Id { get; set; }
        //KONUM
        [Required]
        [Display(Name = "İl")]
        public int il { get; set; }

        [Required]
        [Display(Name = "İlçe")]
        public int ilce { get; set; }

        [Display(Name = "Semt")]
        [Required]
        public int semt { get; set; }

        [Display(Name = "Metrekare")]
        [Required]
        public int metrekare { get; set; }

        [Display(Name = "Fiyat")]
        [Required]
        public int fiyat { get; set; }

        [Display(Name = "Tapu Durumu")]
        [Required]
        public int tapuDurumu { get; set; }

        [Display(Name = "Arsa Tipi")]
        [Required]
        public string arsaTipi { get; set; }

        [Display(Name = "İlgili Belediye")]
        public string ilgiliBelediye { get; set; }

        [Display(Name = "Parsel")]
        public string parsel { get; set; }

        [Display(Name = "Aciklama")]
        public string aciklama { get; set; }


        public bool hasResim { get; set; }
        public bool hasHarita { get; set; }
        public bool hasVideo { get; set; }
    }

    public class arsaMedya
    {
        [Key]
        public int id { get; set; }
        public int arsa_id { get; set; }

        public string resim { get; set; }
        public string harita { get; set; }
        public string video { get; set; }
    }

    public class ilan
    {
        public int id { get; set; }

        [Display(Name = "İlan Numarası")]
        public string ilanNo { get; set; }

        [Display(Name = "İlan tarihi")]
        [DataType(DataType.Date, ErrorMessage = "Lütfen Geçerli Bir Tarih Giriniz")]
        public DateTime ilantarihi { get; set; }

        [Display(Name = "İlan Sahibi")]
        public int ilanSahibi { get; set; }

        [Display(Name = "İlan Türü")]
        [Required]
        public int ilanTuru { get; set; } // 1 => arsa

        [Display(Name = "Takas")]
        public bool takasDurum { get; set; } // 1=> takas yapılabilir


    }
    public class il
    {
        public int Id { get; set; }

        [Display(Name = "İl")]
        public string il_adi { get; set; }
    }

    public class ilce
    {
        public int Id { get; set; }
        public int il_id { get; set; }

        [Display(Name = "İlçe")]
        public string ilce_adi { get; set; }
    }

    public class semt
    {
        public int Id { get; set; }
        public int ilce_id { get; set; }

        [Display(Name = "Semt")]
        public string semt_adi { get; set; }
    }

    public class tapuDurumu
    {
        public int Id { get; set; }

        [Display(Name = "Tapu Durumu")]
        public string durum { get; set; }
    }

    public class ilanSahibi
    {
        public int Id { get; set; }

        [Display(Name = "İlan Sahibinin Adı")]
        [Required]
        public string ilanSahibi_adi { get; set; }

        [Display(Name = "İlan Sahibinin Soyadı")]
        [Required]
        public string ilanSahibi_soyadi { get; set; }

        [Display(Name = "İlan Sahibinin Telefon Numarası")]
        [Required]
        public string ilanSahibi_tel { get; set; }

        [Display(Name = "İlan Sahibinin GSM Numarası")]
        [Required]
        public string ilanSahibi_gsm { get; set; }

        [Display(Name = "İlan Sahibinin Email Adresi")]
        [Required]
        public string ilanSahibi_email { get; set; }

        public int ilanSahibi_uyelikTipi { get; set; }
    }

    public class uyelikTipi
    {
        public int id { get; set; }

        public string uyelikTipi_isim { get; set; }
    }
}