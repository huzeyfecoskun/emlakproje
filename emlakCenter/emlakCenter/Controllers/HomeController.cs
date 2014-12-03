using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using emlakCenter.Models;
using Newtonsoft.Json;
using RijndaelEncryptDecrypt;

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
            return Content(EncryptDecryptUtils.Decrypt(Request.Cookies["ute"].Value,"asss","ddddd","SHA1"));
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

        class SearchObject
        {
            public String imgUrl,content,price,area;
        }

        public ActionResult GetSearchResults(string queryString)
        {
            SearchObject s1 = new SearchObject();
            s1.imgUrl = new Uri(Request.Url, Url.Content("~/Helper/DosyaAdi/?adres=resim-1.jpg&w=200&h=140")).ToString();
            s1.content = "İçerik 1";
            s1.price = "2.000.000 TRY";
            s1.area = "1.000.000";

            SearchObject s2 = new SearchObject();
            s2.imgUrl = new Uri(Request.Url, Url.Content("~/Helper/DosyaAdi/?adres=resim-1.jpg&w=200&h=140")).ToString();
            s2.content = "İçerik 2";
            s2.price = "2.000.000 TRY";
            s2.area = "1.000.000";

            SearchObject s3 = new SearchObject();
            s3.imgUrl = new Uri(Request.Url, Url.Content("~/Helper/DosyaAdi/?adres=resim-1.jpg&w=200&h=140")).ToString();
            s3.content = "İçerik 3";
            s3.price = "2.000.000 TRY";
            s3.area = "1.000.000";

            SearchObject[] result = new SearchObject[3];
            result[0] = s1;
            result[1] = s2;
            result[2] = s3;

            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }

    }
}