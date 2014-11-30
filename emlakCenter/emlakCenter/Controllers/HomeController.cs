using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using emlakCenter.Models;
using Newtonsoft.Json;

namespace emlakCenter.Controllers
{
    public class HomeController : Controller
    {
        private systemDB _db = new systemDB();

        public ActionResult Index()
        {
            return View();
        }

    
        public ActionResult GetIlce(int id)
        {
            var ilce = _db.ilceler.Where(n => n.il_id == id).ToList();
            return Json(JsonConvert.SerializeObject(ilce), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSemt(int id)
        {
            var semt = _db.semtler.Where(n => n.ilce_id == id).ToList();
            return Json(JsonConvert.SerializeObject(semt), JsonRequestBehavior.AllowGet);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Ysfdhjlşwerwe enes ption page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Data()
        {
            return View(_db.iller.ToList());
        }
    }
}