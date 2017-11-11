using BlogProject.Data;
using BlogProject.Models;
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
                Post newPost = new Post();

                return View(newPost);
            }
            else return RedirectToAction("Login", "Accounts");
        }

        [HttpPost]
        public ActionResult Write(Post post)
        {
            var CurrentlyLoggedInUser = (User)Session["User"];

            post.PublishedTime = DateTime.Now;
            post.Author = db.Users.Where(u=>u.ID == CurrentlyLoggedInUser.ID).FirstOrDefault();

            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(post);
        }

        public ActionResult Details(int ID)
        {
            var post = db.Posts.Where(p => p.ID == ID).FirstOrDefault();

            return View(post);
        }
    }
}