using BlogProject.Code;
using BlogProject.Data;
using BlogProject.ViewModels;
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

        public ActionResult Index(int? pageNo, int? posts)
        {
            pageNo = pageNo ?? 1;
            posts = posts ?? Variables.NoOfPosts;

            var take = posts.Value;
            var skip = (pageNo.Value - 1) * posts.Value;
            
            HomeViewModel model = new HomeViewModel();
            model.TotalPosts = db.Posts.Count();
            model.PostsList = db.Posts.OrderByDescending(x=>x.PublishedTime).Skip(skip).Take(take).ToList();
            model.Posts = posts.Value;
            model.PageNo = pageNo.Value;
            model.DisplayNext = model.DisplayPrevious = true;

            if (model.TotalPosts - ((model.PageNo - 1) * model.Posts) <= model.Posts)
            {
                model.DisplayNext = false;
            }

            if (model.PageNo <= 1)
            {
                model.DisplayPrevious = false;
            }

            return View(model);
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