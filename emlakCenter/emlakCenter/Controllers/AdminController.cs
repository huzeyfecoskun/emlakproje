using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace emlakCenter.Controllers
{
    public class AdminController : Controller
    {
        private String[] allowedExtensions = { "jpg", "png", "gif" };
        public ActionResult Index()
        {
            ViewBag.extensions = String.Join(", ", allowedExtensions).ToUpperInvariant(); // GIF instead of GİF
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult UploadImageMethod()
        {
            if (Request.Files.Count != 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    string[] ext = fileName.Split('.');
                    string extention = ext[ext.Length - 1].ToLower();
                    if ( allowedExtensions.Contains(extention) )
                    {
                        string[] arr = fileName.Split('\\');
                        fileName = arr[arr.Length - 1];
                        file.SaveAs(Server.MapPath("~/Content/uploads/" + fileName));
                        //hImage img = new hImage();
                        //img.imageUrl = fileName;
                        //_db.images.Add(img);
                        //_db.SaveChanges();
                        return Content("Success");
                    }

                }
                
            }
            return Content("failed");
        }
    }
}