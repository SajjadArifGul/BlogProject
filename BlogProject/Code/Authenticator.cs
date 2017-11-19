using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Code
{
    public static class Authenticator
    {
        private static User loggedInUser;

        public static bool isLoggedIn
        {
            get {
                loggedInUser = (User)HttpContext.Current.Session["User"];

                if (loggedInUser != null)
                {
                    return true;
                }
                else return false;
            }
        }

        public static void StartUserSession(User user)
        {
            if(user != null)
            {
                HttpContext.Current.Session["User"] = user;
            }
        }

        public static User GetLoggedInUser()
        {
            if (isLoggedIn)
            {
                return loggedInUser;
            }
            else return null;
        }
    }
}