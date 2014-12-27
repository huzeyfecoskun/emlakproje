using System;
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
using System.Data.Entity.Migrations;

namespace emlakCenter.Controllers
{

    [Authorize]
    public class ilansController : Controller
    {
        private systemDB db = new systemDB();

        // GET: ilans
        public async Task<ActionResult> Index()
        {
            return View(await db.ilanlar.ToListAsync());
        }

        // GET: ilans/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ilan ilan = db.ilanlar.Where(n => n.id == id).FirstOrDefault();
            if (ilan == null)
            {
                return HttpNotFound();
            }
            var ilanNo = ilan.ilanNo;
            arsa ars = db.arsalar.Where(n => n.ilanNo == ilanNo).FirstOrDefault();
            return View(ars);
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
            HttpCookie c = new HttpCookie("foto");
            c.Value = ilanNo.ToString();
            Response.SetCookie(c);
            IEnumerable<Medya> m = db.medyalar.Where(n => n.ilanNo == ilanNo).Where(x => x.tip == 0);
            return View(m);
        }

        public async Task<ActionResult> fotografSil(long id)
        {
            Medya m = db.medyalar.Where(n => n.id == id).FirstOrDefault();
            var b = db.medyalar.Where(n => n.tip == MedyaTipi.RESIM).ToList();
            var ars1 = db.arsalar.Where(n => n.ilanNo == m.ilanNo).FirstOrDefault();
            if(b.Count == 1)
            {
                ars1.hasResim = false;
            }
            var ilanNo = m.ilanNo;
            if(m != null)
            {
                db.medyalar.Remove(m);
                db.SaveChanges();
                string filePath = Server.MapPath("~/" + m.content);
                if(System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    //if(System.IO.Directory.EnumerateFiles(filePath).Count() == 0)
                    //{
                    //    System.IO.Directory.Delete(filePath);
                    //}
                }
            }
            else
            {
                return Content("Bir hata meydana geldi");
            }
            if(b.Count == 1)
            {
                System.IO.File.Delete(Server.MapPath("~/Content/Uploads/" + m.ilanNo+"/Thumbs.db"));
                System.IO.Directory.Delete(Server.MapPath("~/Content/Uploads/" + m.ilanNo));
            }
            return Redirect("~/ilans/fotografEkle/?ilanNo=" + ilanNo.ToString());
        }

        public ActionResult haritaSil(long id)
        {
            Medya m = db.medyalar.Where(n => n.id == id).FirstOrDefault();
            var ilanNo = m.ilanNo;
            if (m != null)
            {
                db.medyalar.Remove(m);
                db.SaveChanges();
            }
            else
            {
                return Content("Bir hata meydana geldi");
            }
            return Redirect("~/ilans/haritaEkle/?ilanNo=" + ilanNo.ToString());
        }
        public ActionResult videoSil(long id)
        {
            Medya m = db.medyalar.Where(n => n.id == id).FirstOrDefault();
            var ilanNo = m.ilanNo;
            if (m != null)
            {
                db.medyalar.Remove(m);
                db.SaveChanges();
            }
            else
            {
                return Content("Bir hata meydana geldi");
            }
            return Redirect("~/ilans/videoEkle/?ilanNo=" + ilanNo.ToString());
        }

        [HttpGet]
        public ActionResult haritaEkle(long? ilanNo)
        {
            if(ilanNo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<Medya> m = db.medyalar.Where(n => n.ilanNo == ilanNo).Where(x => x.tip == MedyaTipi.HARITA).ToList();
            return View(m);
        }
        [HttpPost]
        public ActionResult haritaEkle(Medya m)
        {
            Medya medya = new Medya();
            medya.tip = MedyaTipi.HARITA;
            medya.ilanNo = m.ilanNo;
            medya.content = m.content;
            db.medyalar.AddOrUpdate(n => n.tip, medya);
            db.SaveChanges();

            var ilanNo = m.ilanNo;
            return Redirect("~/ilans/haritaEkle/?ilanNo=" + ilanNo.ToString());
        }
        [HttpGet]
        public ActionResult videoEkle(long? ilanNo)
        {
            if (ilanNo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<Medya> video = db.medyalar.Where(n => n.ilanNo == ilanNo && n.tip == MedyaTipi.VIDEO).ToList();
            return View(video);
        }
        [HttpPost]
        public ActionResult videoEkle(Medya m)
        {
            Medya medya = new Medya();
            medya.tip = MedyaTipi.VIDEO;
            medya.ilanNo = m.ilanNo;
            medya.content = m.content;
            db.medyalar.AddOrUpdate(n => n.tip, medya);
            db.SaveChanges();
            var ilanNo = m.ilanNo;
            return Redirect("~/ilans/videoEkle/?ilanNo=" + ilanNo.ToString());
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
            var medya = db.medyalar.Where(n => n.ilanNo == ilan.ilanNo).ToList();
            db.medyalar.RemoveRange(medya);
            arsa arsa = db.arsalar.Where(n => n.ilanNo == ilan.ilanNo).FirstOrDefault();
            db.ilanlar.Remove(ilan);
            db.arsalar.Remove(arsa);
            await db.SaveChangesAsync();

            var ilanNo = ilan.ilanNo;
            if(System.IO.Directory.Exists(Server.MapPath("~/Content/Uploads/" + ilanNo)))
            {
                var dirs = System.IO.Directory.GetFiles(Server.MapPath("~/Content/Uploads/" + ilanNo));
                foreach(var items in dirs)
                {
                    System.IO.File.Delete(items);
                }
                System.IO.Directory.Delete(Server.MapPath("~/Content/Uploads/" + ilanNo));
                return RedirectToAction("index");
            }
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
