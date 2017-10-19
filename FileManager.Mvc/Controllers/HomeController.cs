using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace FileManager.Mvc.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
           string directory = @"D:\Rodion\Storage";

            //string directory = "~/App_Data/Storage";

            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                file.SaveAs(Path.Combine(directory, fileName));
            }

            return RedirectToAction("Index");
        }
    }
}