using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogProject.Models;

namespace BlogProject.ViewModels
{
    public class AuthorViewModel
    {
        public string Name { get; set; }
        public City City { get; set; }
        public string AddressDetails { get; set; }
        public List<Post> Posts { get; set; }
    }
}