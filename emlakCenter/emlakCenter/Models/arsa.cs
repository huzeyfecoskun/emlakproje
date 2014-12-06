using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace emlakCenter.Models
{
    public enum ilanTipi
    {
        ARSA=0,
        EV=1, 
        ARABA=2, 
        MOTOSIKLET=3, 
        ELEKTRONIK=4
    };

    public enum ArsaTipi { 
        SATILIK=0,
        KIRALIK=1
    };

    public enum VarYok
    {
        YOK= 0,
        VAR = 1
    };

    public enum MedyaTipi
    {
        RESIM=0,
        VIDEO=1,
        HARITA=2
    };

    public enum IlanSahibi
    {
        SAHIBINDEN = 0,
        EMLAKCIDAN = 1,
        BANKADAN = 2,
        MUTEAHHITTEN = 3
    };

    public enum Uygunluk
    {
        UYGUNDEGIL = 0,
        UYGUN = 1
    };

    public enum TapuDurumuDef
    {
        ARSA=0,
        KAT_IRTIFAKI=1,
        KAT_MULKIYETI=2,
        HISSELI_TAPU=3,
        MUSTAKIL_PARSEL=4,
        TAHSIS=5,
        ZILLIYET=6,
        BILINMIYOR=7
    };
    public class arsa
    {
        public long Id { get; set; }

        public long ilanNo { get; set; }
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
        public TapuDurumuDef tapuDurumu { get; set; }

        [Display(Name = "Arsa Tipi")]
        [Required]
        public ArsaTipi arsaTipi { get; set; }

        [Display(Name = "Parsel")]
        public string parsel { get; set; }

        [Display(Name = "Ada")]
        public string ada { get; set; }

        [Display(Name = "Kat Karşılığı")]
        public Uygunluk katKarsiligi { get; set; }

        [Display(Name = "Aciklama")]
        public string aciklama { get; set; }

        public bool hasResim { get; set; }
        public bool hasHarita { get; set; }
        public bool hasVideo { get; set; }
    }

    public class Medya
    {
        public long id { get; set; }
        public long ilanNo { get; set; }
        public string content { get; set; }
        public MedyaTipi tip { get; set; }
    }

    public class ilan
    {
        public long id { get; set; }

        public ilanTipi tip { get; set; }

        public int IlanSahibiId { get; set; }

        [Display(Name = "İlan Numarası")]
        public long ilanNo { get; set; }

        [Display(Name = "İlan tarihi")]
        [DataType(DataType.Date, ErrorMessage = "Lütfen Geçerli Bir Tarih Giriniz")]
        public DateTime ilantarihi { get; set; }

        [Display(Name = "İlan Sahibi")]
        public IlanSahibi ilanSahibi { get; set; }

        [Display(Name = "Takas")]
        public Uygunluk takasDurum { get; set; } // 1=> takas yapılabilir

        [Display(Name = "Krediye Uygunluk")]
        public Uygunluk krediyeUygunluk { get; set; } // 1=> Uygun

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