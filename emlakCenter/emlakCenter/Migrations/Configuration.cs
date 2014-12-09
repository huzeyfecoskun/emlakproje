namespace emlakCenter.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<emlakCenter.Models.systemDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(emlakCenter.Models.systemDB context)
        {
            ilanSahibi ilanSahip = new ilanSahibi();
            ilanSahip.ilanSahibi_adi = "Halid";
            ilanSahip.ilanSahibi_email = "halidsenyigit@gmail.com";
            ilanSahip.ilanSahibi_gsm = "0 507 889 56 04";
            ilanSahip.ilanSahibi_soyadi = "Þenyiðit";
            ilanSahip.ilanSahibi_tel = "05078895604";
            ilanSahip.ilanSahibi_uyelikTipi = UyelikTipi.ADMIN;
            context.ilanSahipleri.AddOrUpdate(p => p.ilanSahibi_adi, ilanSahip);

            Models.arsa arsa3 = new Models.arsa();
            arsa3.il = 60;
            arsa3.ilce = 920;
            arsa3.semt = 4262;
            arsa3.metrekare = 1000000;
            arsa3.fiyat = 1000;
            arsa3.tapuDurumu = TapuDurumuDef.ARSA;
            arsa3.arsaTipi = ArsaTipi.SATILIK;
            arsa3.aciklama = "Ýçerik 3";
            arsa3.ilanNumarasi = 1001;
            arsa3.ada = "12";
            arsa3.parsel = "1201";
            context.arsalar.AddOrUpdate(p => p.aciklama, arsa3);

            ilan ilan1 = new ilan();
            ilan1.ilanNo = 1001;
            ilan1.ilanSahibi = IlanSahibi.EMLAKCIDAN;
            ilan1.IlanSahibiId = 1;
            ilan1.ilantarihi = new DateTime(System.DateTime.Now.Ticks);
            ilan1.krediyeUygunluk = Uygunluk.UYGUN;
            ilan1.takasDurum = Uygunluk.UYGUN;
            ilan1.tip = ilanTipi.ARSA;

            context.ilanlar.AddOrUpdate(p => p.ilanNo, ilan1);
        }
    }
}
