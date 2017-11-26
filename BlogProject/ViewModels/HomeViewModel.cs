using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogProject.ViewModels
{
    public class HomeViewModel
    {
        public List<Post> PostsList { get; set; }

        public int TotalPosts { get; set; }

        public int PageNo { get; set; }
        public int Posts { get; set; }

        public bool DisplayNext { get; set; }
        public bool DisplayPrevious { get; set; }
    }
}