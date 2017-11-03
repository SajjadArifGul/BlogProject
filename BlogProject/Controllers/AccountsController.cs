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
    }
}