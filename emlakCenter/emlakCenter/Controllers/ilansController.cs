﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using emlakCenter.Models;
using Newtonsoft.Json;

namespace emlakCenter.Controllers
{
    public class ilansController : Controller
    {
        private systemDB db = new systemDB();

        // GET: ilans
        public async Task<ActionResult> Index()
        {
            return View(await db.ilanlar.ToListAsync());
        }

        // GET: ilans/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ilan ilan = await db.ilanlar.FindAsync(id);
            if (ilan == null)
            {
                return HttpNotFound();
            }
            return View(ilan);
        }

        // GET: ilans/Create
        public ActionResult Create()
        {
            if (Request.Cookies["iln"] != null)
            {
                return RedirectToAction("ArsaOlustur");
            }
            return View();
        }

        // POST: ilans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,tip,ilantarihi,ilanSahibi,takasDurum,krediyeUygunluk")] ilan ilan)
        {
            if(Request.Cookies["iln"] != null)
            {
                return RedirectToAction("ArsaOlustur");
            }
            if (ModelState.IsValid)
            {
                HttpCookie c = new HttpCookie("iln");
                c.Value = Helper.encrypt(JsonConvert.SerializeObject(ilan));
                Response.SetCookie(c);
                //db.ilanlar.Add(ilan);
                //await db.SaveChangesAsync();
                return RedirectToAction("ArsaOlustur");
            }

            return View(ilan);
        }

         public ActionResult ArsaOlustur()
        {
            if (Request.Cookies["iln"] != null && Request.Cookies["ars"] != null)
            {
                return RedirectToAction("Onizleme");
            }
            return View();
        }

        [HttpPost]
        public ActionResult ArsaOlustur(arsa data)
        {
            if (Request.Cookies["iln"] != null && Request.Cookies["ars"] != null)
            {
                return RedirectToAction("Onizleme");
            }
            if (ModelState.IsValid)
            {
                HttpCookie c = new HttpCookie("ars");
                c.Value = Helper.encrypt(JsonConvert.SerializeObject(data));
                Response.SetCookie(c);
                return RedirectToAction("Onizleme");
            }
            return View();
        }

        public ActionResult Onizleme()
        {
            if (Request.Cookies["iln"] == null || Request.Cookies["ars"] == null)
            {
                return RedirectToAction("Index");
            }
            ilan iln = JsonConvert.DeserializeObject<ilan>(Helper.decrypt(Request.Cookies["iln"].Value));
            arsa ars = JsonConvert.DeserializeObject<arsa>(Helper.decrypt(Request.Cookies["ars"].Value));
            var d = Tuple.Create(iln, ars);
            return View(d);
        }

        public ActionResult FotografEkle(long ilanNo)
        {
            return View();
        }
        public ActionResult IlanIptal()
        {
            if (Request.Cookies["iln"] != null)
            {
                HttpCookie myCookie = new HttpCookie("iln");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }

            if (Request.Cookies["ars"] != null)
            {
                HttpCookie myCookie = new HttpCookie("ars");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            return RedirectToAction("Index");
        }
        public ActionResult IlanKayit()
        {
            if (Request.Cookies["iln"] == null || Request.Cookies["ars"] == null)
            {
                return RedirectToAction("Index");
            }
            ilan iln = JsonConvert.DeserializeObject<ilan>(Helper.decrypt(Request.Cookies["iln"].Value));
            arsa ars = JsonConvert.DeserializeObject<arsa>(Helper.decrypt(Request.Cookies["ars"].Value));

            if(ModelState.IsValid)
            {
                ilan ilanEkle = new ilan();
                long ilanNo = Helper.UniqueIlan();
                ilanEkle.ilanNo = ilanNo;
                ilanEkle.ilanSahibi = iln.ilanSahibi;
                ilanEkle.IlanSahibiId = iln.IlanSahibiId;
                ilanEkle.ilantarihi = DateTime.Now;
                ilanEkle.krediyeUygunluk = iln.krediyeUygunluk;
                ilanEkle.takasDurum = iln.takasDurum;
                ilanEkle.tip = iln.tip;

                db.ilanlar.Add(ilanEkle);
                db.SaveChanges();

                arsa arsaEkle = new arsa();
                ars.hasHarita = false;
                ars.hasResim = false;
                ars.hasVideo = false;
                ars.ilanNo = ilanNo;
                db.arsalar.Add(ars);
                db.SaveChanges();

                HttpCookie c = new HttpCookie("ars");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.SetCookie(c);

                HttpCookie c2 = new HttpCookie("iln");
                c2.Expires = DateTime.Now.AddDays(-1);
                Response.SetCookie(c2);

                ViewBag.ilanNo = ilanNo.ToString();
            }
            
            return View();
        }

        // GET: ilans/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ilan ilan = await db.ilanlar.FindAsync(id);
            if (ilan == null)
            {
                return HttpNotFound();
            }
            return View(ilan);
        }

        // POST: ilans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,tip,ilanNo,ilantarihi,ilanSahibi,takasDurum,krediyeUygunluk")] ilan ilan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ilan).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ilan);
        }

        // GET: ilans/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ilan ilan = await db.ilanlar.FindAsync(id);
            if (ilan == null)
            {
                return HttpNotFound();
            }
            return View(ilan);
        }

        // POST: ilans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            ilan ilan = await db.ilanlar.FindAsync(id);
            db.ilanlar.Remove(ilan);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
