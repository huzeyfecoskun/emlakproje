using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using emlakCenter.Models;
using System.IO;

namespace emlakCenter.Controllers
{
    public class HelperController : Controller
    {
        systemDB _db = new systemDB();

        protected override void Dispose(bool disposing)
        {
            if (_db != null) _db.Dispose();
            base.Dispose(disposing);
        }
        // GET: Helper
        public ActionResult resim(int id, int w, int h)
        {
            //Dosya adresi
            var str = _db.ArsaMedya.Where(n => n.id == id).FirstOrDefault();
            //Databaseden resmi çekelim
            Image tmp = Image.FromFile(Server.MapPath("~/Content/img/ilan-resim/resim-1.jpg"));
            //width ve height ten bitmap oluşturalım
            Bitmap bitmap = new Bitmap(w, h);
            //Graphics oluştur
            Graphics g = Graphics.FromImage((Image)bitmap);
            //Yeni boyutlara yazdıralım
            g.DrawImage(tmp, new Rectangle(0, 0, w, h));
            //Memory stream e bitmapı yaz
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //Resim olarak döndür
            return base.File(ms.ToArray(), "image/png");
        }


        public ActionResult DosyaAdi(string adres, int w, int h)
        {
            //Databaseden resmi çekelim
            Image tmp = Image.FromFile(Server.MapPath("~/Content/img/ilan-resim/"+adres));
            //width ve height ten bitmap oluşturalım
            Bitmap bitmap = new Bitmap(w, h);
            //Graphics oluştur
            Graphics g = Graphics.FromImage((Image)bitmap);
            //Yeni boyutlara yazdıralım
            g.DrawImage(tmp, new Rectangle(0, 0, w, h));
            //Memory stream e bitmapı yaz
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //Resim olarak döndür
            return base.File(ms.ToArray(), "image/png");
        }
    }
}