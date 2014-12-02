using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace emlakCenter.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Upload(HttpPostedFile photo)
        {
            if (Request.Files.Count != 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = Request.Files[i];
                    int fileSize = file.ContentLength;

                    string fileName = file.FileName;
                    string[] arr = fileName.Split('\\');
                    fileName = arr[arr.Length - 1];
                    file.SaveAs(Server.MapPath("~/Content/Uploads/" + fileName));
                }
                return Content("Success");
            }
            return Content("failed");

        }
    }
}