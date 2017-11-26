using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Helpers
{
    public static class URLHelper
    {
        /// <summary>
        /// Generates a fully qualified URL to an action method by using
        /// the specified action name, controller name and route values.
        /// </summary>
        /// <param name="url">The URL helper.</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">The route values.</param>
        /// <returns>The absolute URL.</returns>
        public static string AbsoluteAction(this UrlHelper url,
            string actionName, string controllerName, object routeValues = null)
        {
            string scheme = url.RequestContext.HttpContext.Request.Url.Scheme;

            return url.Action(actionName, controllerName, routeValues, scheme);
        }
        
        public static string PostDetailsURL(this UrlHelper helper, int postID, string postURL)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("PostDetails", new
            {
                postID = postID,
                postURL = postURL
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string PostsNavigate(this UrlHelper helper, int pageNo, int posts)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("PostsNavigate", new
            {
                pageNo = pageNo,
                posts = posts
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

        public static string AuthorPostsNavigate(this UrlHelper helper, int authorID, int pageNo, int posts)
        {
            string routeURL = string.Empty;

            routeURL = helper.RouteUrl("AuthorPostsNavigate", new
            {
                authorID = authorID,
                pageNo = pageNo,
                posts = posts
            });

            routeURL = HttpUtility.UrlDecode(routeURL, System.Text.Encoding.UTF8);
            return routeURL.ToLower();
        }

    }
}