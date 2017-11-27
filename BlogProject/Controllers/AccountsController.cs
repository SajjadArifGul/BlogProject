using BlogProject.Code;
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
    public class AccountsController : Controller
    {
        DataContext db = new DataContext();
        
        // GET: Accounts
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();

            model.Countries = db.Countries.OrderBy(x=>x.Name).ToList();
            model.Cities = db.Cities.OrderBy(x => x.Name).ToList();

            model.DefaultCountry = 2;
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User newRegisteredUser = new User();

                newRegisteredUser.Name = model.Name;
                newRegisteredUser.Email = model.Email;
                newRegisteredUser.Password = model.Password;
                newRegisteredUser.isStudent = model.isStudent;
                newRegisteredUser.isFullTimeJob = model.isFullTimeJob;
                newRegisteredUser.isPartTimeJob = model.isPartTimeJob;
                newRegisteredUser.AddressDetails = model.AddressDetails;
                newRegisteredUser.Gender = model.Gender;
                newRegisteredUser.City = db.Cities.Where(c => c.ID == model.CityID).FirstOrDefault();

                db.Users.Add(newRegisteredUser);
                db.SaveChanges();

                Session["User"] = newRegisteredUser;

                return RedirectToAction("Index", "Home");
            }
            else {

                model.Countries = db.Countries.OrderBy(x => x.Name).ToList();
                model.Cities = db.Cities.OrderBy(x => x.Name).ToList();

                model.DefaultCountry = 2;

                return View(model);
            }
        }
        
        public ActionResult Logout()
        {
            //Session["User"] = null;

            Session.Clear();
            //Session.RemoveAll();

            return RedirectToAction("Index","Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var UserFromDB = db.Users.Where(u=>u.Email.Equals(model.Email)).FirstOrDefault();

                if(UserFromDB != null)
                {
                    if(UserFromDB.Password.Equals(model.Password))
                    {
                        Session["User"] = UserFromDB;

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Your password does not match.");

                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "No user exist with this email.");

                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
        
        public ActionResult Profile()
        {
            var CurrentlyLoggedInUser = (User)Session["User"];

            if (CurrentlyLoggedInUser != null)
            {
                ProfileViewModel profileViewModel = new ProfileViewModel();

                profileViewModel.Cities = db.Cities.ToList();
                profileViewModel.Name = CurrentlyLoggedInUser.Name;
                profileViewModel.isStudent = CurrentlyLoggedInUser.isStudent;
                profileViewModel.isPartTimeJob = CurrentlyLoggedInUser.isPartTimeJob;
                profileViewModel.isFullTimeJob = CurrentlyLoggedInUser.isFullTimeJob;
                profileViewModel.Gender = CurrentlyLoggedInUser.Gender;
                profileViewModel.CityID = CurrentlyLoggedInUser.City.ID;
                profileViewModel.AddressDetails = CurrentlyLoggedInUser.AddressDetails;

                profileViewModel.Countries = db.Countries.ToList();
                profileViewModel.Cities = db.Cities.ToList();

                profileViewModel.DefaultCountry = CurrentlyLoggedInUser.City.Country.ID;
                
                return View(profileViewModel);
            }
            else return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Profile(ProfileViewModel model)
        {
            var CurrentlyLoggedInUser = (User)Session["User"];

            if(CurrentlyLoggedInUser != null)
            {
                if (ModelState.IsValid)
                {
                    User loggedInUser = db.Users.Where(u=>u.ID == CurrentlyLoggedInUser.ID).FirstOrDefault();

                    loggedInUser.Name = model.Name;
                    loggedInUser.isStudent = model.isStudent;
                    loggedInUser.isFullTimeJob = model.isFullTimeJob;
                    loggedInUser.isPartTimeJob = model.isPartTimeJob;
                    loggedInUser.AddressDetails = model.AddressDetails;
                    loggedInUser.City = db.Cities.Where(c => c.ID == model.CityID).FirstOrDefault();
                    loggedInUser.Gender = model.Gender;
                    
                    db.Entry(loggedInUser).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    Session["User"] = loggedInUser;
                    
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    model.Countries = db.Countries.OrderBy(x => x.Name).ToList();
                    model.Cities = db.Cities.OrderBy(x => x.Name).ToList();

                    model.DefaultCountry = CurrentlyLoggedInUser.City.Country.ID;

                    return View(model);
                }
            }
            else return RedirectToAction("Login");
        }

        public ActionResult Author(int AuthorID, int? pageNo, int? posts)
        {
            pageNo = pageNo ?? 1;
            posts = posts ?? Variables.NoOfPosts;

            var take = posts.Value;
            var skip = (pageNo.Value - 1) * posts.Value;

            var author = db.Users.Where(a=>a.ID == AuthorID).FirstOrDefault();

            if (author != null)
            {
                AuthorViewModel authorViewModel = new AuthorViewModel();
                authorViewModel.Author = author;
                authorViewModel.Name = author.Name;
                authorViewModel.City = author.City;
                authorViewModel.AddressDetails = author.AddressDetails;
                
                authorViewModel.TotalPosts = db.Posts.Where(p => p.Author.ID == AuthorID).Count();
                authorViewModel.PostsList = db.Posts.Where(p => p.Author.ID == AuthorID).OrderByDescending(x => x.PublishedTime).Skip(skip).Take(take).ToList();
                authorViewModel.Posts = posts.Value;
                authorViewModel.PageNo = pageNo.Value;
                authorViewModel.DisplayNext = authorViewModel.DisplayPrevious = true;

                if (authorViewModel.TotalPosts - ((authorViewModel.PageNo - 1) * authorViewModel.Posts) <= authorViewModel.Posts)
                {
                    authorViewModel.DisplayNext = false;
                }

                if (authorViewModel.PageNo <= 1)
                {
                    authorViewModel.DisplayPrevious = false;
                }
                
                return View(authorViewModel);
            }
            else return RedirectToAction("Index", "Home");
        }

        public ActionResult GetCities(int CountryID)
        {
            var cities = db.Cities.Where(c=>c.Country.ID == CountryID).ToList();

            ViewBag.Cities = cities;

            return View("_GetCities", "");
        }

        public JsonResult GetCitiesByJSON(int CountryID)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            var cities = db.Cities.Where(c => c.Country.ID == CountryID).OrderBy(x => x.Name).ToList();

            result.Data = cities;

            return result;
        }
    }
}