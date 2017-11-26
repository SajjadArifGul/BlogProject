using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BlogProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AboutUs",
                url: "about-us",
                defaults: new { controller = "Home", action = "About"}
            );

            routes.MapRoute(
                name: "ContactUs",
                url: "contact-us",
                defaults: new { controller = "Home", action = "Contact" }
            );

            routes.MapRoute(
                name: "Register",
                url: "register",
                defaults: new { controller = "Accounts", action = "Register" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Accounts", action = "Login" }
            );

            routes.MapRoute(
                name: "Profile",
                url: "me/profile",
                defaults: new { controller = "Accounts", action = "Profile" }
            );
            
            routes.MapRoute(
                name: "Author",
                url: "authors",
                defaults: new { controller = "Accounts", action = "Author" }
            );

            routes.MapRoute(
                name: "AuthorPostsNavigate",
                url: "authors",
                defaults: new
                {
                    controller = "Accounts",
                    action = "Author",
                }
            );

            routes.MapRoute(
                name: "NewPost",
                url: "new-post",
                defaults: new { controller = "Posts", action = "Write" }
            );

            routes.MapRoute(
                name: "PostDetails",
                url: "post/{postID}/{postURL}",
                defaults: new {
                    controller = "Posts",
                    action = "Details",
                    postURL = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "PostsNavigate",
                url: "{controller}/{action}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
