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
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.ilanSahipleri.AddOrUpdate(
                p => p.ilanSahibi_adi,
                new ilanSahibi { 
                    ilanSahibi_adi = "Halid",
                    ilanSahibi_soyadi= "Þenyiðit",
                    ilanSahibi_email="halidsenyigit@gmail.com",
                    ilanSahibi_gsm="0 555 555 55 55",
                    ilanSahibi_tel="0 312 222 22 22",
                    ilanSahibi_uyelikTipi=1 
                }
                );

            context.ilanlar.AddOrUpdate(
                p => p.ilanNo,
                new ilan { 
                    ilanNo="1000",
                    ilanSahibi=1,
                    ilantarihi=System.DateTime.Now.Date,
                    ilanTuru=1,
                    takasDurum= true
                }
                );

            Models.arsa arsa1 = new Models.arsa();
            arsa1.il = 60;
            arsa1.ilce = 920;
            arsa1.semt = 4262;
            arsa1.metrekare = 1000000;
            arsa1.fiyat = 1000;
            arsa1.tapuDurumu = 1;
            arsa1.arsaTipi = "Ýmara açýk?";
            arsa1.aciklama = "Ýçerik 1";

            Models.arsa arsa2 = new Models.arsa();
            arsa2.il = 60;
            arsa2.ilce = 920;
            arsa2.semt = 4262;
            arsa2.metrekare = 1000000;
            arsa2.fiyat = 1000;
            arsa2.tapuDurumu = 1;
            arsa2.arsaTipi = "Ýmara açýk?";
            arsa2.aciklama = "Ýçerik 2";

            Models.arsa arsa3 = new Models.arsa();
            arsa3.il = 60;
            arsa3.ilce = 920;
            arsa3.semt = 4262;
            arsa3.metrekare = 1000000;
            arsa3.fiyat = 1000;
            arsa3.tapuDurumu = 1;
            arsa3.arsaTipi = "Ýmara açýk?";
            arsa3.aciklama = "Ýçerik 3";
            context.arsalar.AddOrUpdate(arsa1, arsa2, arsa3);

            
        }

    }
}
