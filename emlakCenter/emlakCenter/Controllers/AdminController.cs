﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using emlakCenter.Models;
using System.Data.Entity;
using System.Web.Security;
namespace emlakCenter.Controllers
{

    public class AdminController : Controller
    {
        private systemDB db = new systemDB();
        protected override void Dispose(bool disposing)
        {
            if (db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private String[] allowedExtensions = { "jpg", "png", "gif" };

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.extensions = String.Join(", ", allowedExtensions).ToUpperInvariant(); // GIF instead of GİF
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string userid, string password)
        {

            var adm = db.adminler.Where(n => n.password == password && n.username == userid).FirstOrDefault();
            if(adm != null)
            {
                FormsAuthentication.SetAuthCookie(userid, false);
                return RedirectToAction("index");
            }
            return View();
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("login");
        }

        [Authorize]
        public ActionResult ArsaDuzenle(long id, int i)
        {
            var arsa = db.arsalar.Where(n => n.ilanNo == id).FirstOrDefault();
            HttpCookie c = new HttpCookie("i");
            c.Value = i.ToString();
            Response.SetCookie(c);
            return View(arsa);
        }
        [HttpPost]
        [Authorize]
        public ActionResult ArsaDuzenle(arsa a)
        {

            var k = db.arsalar.Where(n => n.ilanNo == a.ilanNo).FirstOrDefault();
            k.aciklama = a.aciklama;
            k.ada = a.ada;
            k.arsaTipi = a.arsaTipi;
            k.fiyat = a.fiyat;
            k.il = a.il;
            k.ilce = a.ilce;
            k.katKarsiligi = a.katKarsiligi;
            k.metrekare = a.metrekare;
            k.parsel = a.parsel;
            k.semt = a.semt;
            k.tapuDurumu = a.tapuDurumu;

            db.SaveChanges();

            var i = Request.Cookies["i"].Value;
            return Redirect("~/Ilans/Edit/" + i);
        }
        [HttpPost]
        [Authorize]
        public ActionResult UploadImageMethod(Medya m)
        {
            if (Request.Files.Count != 0)
            {
                string ilanNumarasi = Request.Cookies["foto"].Value;
                long ilanNo = long.Parse(ilanNumarasi);
                HttpCookie c = new HttpCookie("foto");
                c.Expires = DateTime.Now.AddDays(-1d);
                Response.SetCookie(c);
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    string[] ext = fileName.Split('.');
                    string extention = ext[ext.Length - 1].ToLower();
                    if (allowedExtensions.Contains(extention))
                    {
                        string[] arr = fileName.Split('\\');
                        fileName = Models.Helper.UniqueIlan().ToString();
                        var path = Server.MapPath("~/Content/uploads/" + ilanNo.ToString());
                        System.IO.Directory.CreateDirectory(path);
                        file.SaveAs(Server.MapPath("~/Content/uploads/" + ilanNo.ToString() + "/" + fileName + "." + extention));
                        Medya medya = new Medya();
                        medya.ilanNo = ilanNo;
                        medya.content = "Content/uploads/" + ilanNo.ToString() + "/" + fileName + "." + extention;
                        db.medyalar.Add(medya);
                    }
                }
                arsa arsaEkle = db.arsalar.Where(n => n.ilanNo == ilanNo).FirstOrDefault();
                arsaEkle.hasResim = true;
                db.Entry(arsaEkle).State = EntityState.Modified;
                db.SaveChanges();
                return Content("Success");   
            }
            return Content("failed");
        }
    }
}