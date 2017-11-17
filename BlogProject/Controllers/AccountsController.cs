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
        public ActionResult GetRegister()
        {
            RegisterViewModel model = new RegisterViewModel();

            model.Cities = db.Cities.ToList();

            return View(model);
        }

        // GET: Accounts
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();

            model.Countries = db.Countries.ToList();
            model.Cities = db.Cities.ToList();

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
                newRegisteredUser.City = db.Cities.Where(c => c.ID == model.CityID).FirstOrDefault();

                db.Users.Add(newRegisteredUser);
                db.SaveChanges();

                Session["User"] = newRegisteredUser;

                return RedirectToAction("Index", "Home");
            }
            else {

                model.Cities = db.Cities.ToList();

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
            }

            model.Cities = db.Cities.ToList();



            return View(model);
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

            var cities = db.Cities.Where(c => c.Country.ID == CountryID).ToList();

            result.Data = cities;

            return result;
        }
    }
}