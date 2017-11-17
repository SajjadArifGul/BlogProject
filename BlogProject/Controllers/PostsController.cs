using BlogProject.Data;
using BlogProject.Models;
using BlogProject.ViewModels;
using System;
using System.Collections.Generic;
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
        public ActionResult Write(WritePostViewModel model)
        {
            var CurrentlyLoggedInUser = (User)Session["User"];
            
            if (ModelState.IsValid)
            {
                Post newPost = new Post();

                newPost.Title = model.Title;
                newPost.Description = model.Description;
                newPost.PublishedTime = DateTime.Now;
                newPost.Author = db.Users.Where(u => u.ID == CurrentlyLoggedInUser.ID).FirstOrDefault();

                db.Posts.Add(newPost);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult Details(int ID)
        {
            var post = db.Posts.Where(p => p.ID == ID).FirstOrDefault();

            return View(post);
        }
    }
}