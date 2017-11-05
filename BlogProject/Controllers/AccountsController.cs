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

            model.Cities = db.Cities.ToList();
            
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
        
    }
}