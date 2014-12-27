using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using emlakCenter.Models;
using Newtonsoft.Json;
using RijndaelEncryptDecrypt;
using System.Text;

namespace emlakCenter.Controllers
{
    public class HomeController : Controller
    {
        private systemDB _db = new systemDB();

        public ActionResult Index()
        {
            HttpCookie c = new HttpCookie("ute");
            c.Value = EncryptDecryptUtils.Encrypt("halid", "asss", "ddddd", "SHA1");
            c.Expires = DateTime.Now.AddDays(1);
            Response.SetCookie(c);
            HttpContext.Session["ute"] = "aaa";
            return View();
        }

        public ActionResult cookieSil()
        {
            HttpCookie c = new HttpCookie("ute");
            c.Expires = DateTime.Now.AddDays(-1);
            Response.SetCookie(c);
            return Content("silindi");
        }

        public ActionResult CookieOku()
        {
            return Content(EncryptDecryptUtils.Decrypt(Request.Cookies["ute"].Value, "asss", "ddddd", "SHA1"));
        }
        public ActionResult GetIlce(int? id)
        {
            if (id == null) id = 1;
            var ilce = _db.ilceler.Where(n => n.il_id == id).ToList();
            return Json(JsonConvert.SerializeObject(ilce), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSemt(int id)
        {
            var semt = _db.semtler.Where(n => n.ilce_id == id).ToList();
            return Json(JsonConvert.SerializeObject(semt), JsonRequestBehavior.AllowGet);
        }

        class QueryObject
        {
            public String semtID; // TODO: arama haritası tamamlanıp gerekli alanlarla doldurulacak
        }
        class SearchObject
        {
            public String imgUrl, aciklama, fiyat, metrekare;
        }

        [HttpPost]
        public ActionResult GetSearchResults(QueryModel query)
        {
            // TODO: query ye göre arama yapılacak ve sonuçlar json olarak döndürülecek

            var results = _db.arsalar.ToList();
            if(query.fiyatStart == 0 && query.fiyatEnd == 0)
            {
                results = results.Where(n => n.fiyat < query.fiyatEnd && n.fiyat > query.fiyatStart).ToList();
            }
            if(query.metrekareStart != 0 && query.metrekareEnd != 0)
            {
                results = results.Where(n => n.metrekare < query.metrekareEnd && n.metrekare > query.metrekareStart).ToList();
            }
            if(query.il != 0)
            {
                results = results.Where(n => n.il == query.il).ToList();
            }
            if (query.ilce != 0)
            {
                results = results.Where(n => n.ilce == query.ilce).ToList();
            }
            if (query.semt != 0)
            {
                results = results.Where(n => n.semt == query.semt).ToList();
            }
            if (query.tarih != "")
            {
               // tarih iptal
            }
            if(query.video)
            {
                results = results.Where(n => n.hasVideo == query.video).ToList();
            }
            if (query.resim)
            {
                results = results.Where(n => n.hasResim == query.resim).ToList();
            }
            if (query.harita)
            {
                results = results.Where(n => n.hasHarita == query.harita).ToList();
            }
            if(query.keyword != "")
            {
                // Bura biraz karışık
            }
            

            return PartialView("GetSearchResults",results);
        }

        public ActionResult Ilan(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("index", "home");
            }
            else
            {
                return View(_db.ilanlar.Where(n => n.id == id).FirstOrDefault());
            }
        }

    }
}