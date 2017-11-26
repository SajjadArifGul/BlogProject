using BlogProject.Data;
using BlogProject.Models;
using BlogProject.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Controllers
{
    public class PostsController : Controller
    {
        DataContext db = new DataContext();

        // GET: Posts
        public ActionResult Write()
        {
            var CurrentlyLoggedInUser = (User)Session["User"];

            if (CurrentlyLoggedInUser != null)
            {
                WritePostViewModel model = new WritePostViewModel();

                return View(model);
            }
            else return RedirectToAction("Login", "Accounts");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Write(WritePostViewModel model)
        {
            var CurrentlyLoggedInUser = (User)Session["User"];
            
            if (ModelState.IsValid)
            {
                Post newPost = new Post();

                newPost.Title = model.Title;
                newPost.Summary = model.Summary;
                newPost.Description = model.Description;
                newPost.Image = db.Images.Where(x => x.ID == model.ImageID).FirstOrDefault();

                newPost.PublishedTime = DateTime.Now;
                newPost.Author = db.Users.Where(u => u.ID == CurrentlyLoggedInUser.ID).FirstOrDefault();

                db.Posts.Add(newPost);
                db.SaveChanges();

                return RedirectToAction("Details", new { postID = newPost.ID, postURL = newPost.URL });
            }

            return View(model);
        }

        public ActionResult Details(int postID, string postURL)
        {
            var post = db.Posts.Where(p => p.ID == postID).FirstOrDefault();

            return View(post);
        }

        public JsonResult UploadImage()
        {
            JsonResult result = new JsonResult();

            try
            {
                //image upload to server hosting folder
                var file = Request.Files[0];

                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Images/"), fileName);

                file.SaveAs(path);

                //now save image to db as well
                Image newImage = new Image();
                newImage.Source = fileName;

                db.Images.Add(newImage);
                db.SaveChanges();

                result.Data = new { Success = true, ImagePath = fileName, ImageID = newImage.ID };
            }
            catch 
            {
                result.Data = new { success = false};
            }

            return result;
        }
    }
}