using BlogProject.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();

        public ActionResult Index()
        {
            var allPosts = db.Posts.ToList();
            return View(allPosts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult Upload()
        {
            JsonResult resulr = new JsonResult();

            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                var fileName = Path.GetFileName(file.FileName);

                var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                file.SaveAs(path);

                resulr.Data = new { success = true, path = path };
            }

            return resulr;
        }
    }
}