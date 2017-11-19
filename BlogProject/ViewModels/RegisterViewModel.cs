using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogProject.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50, ErrorMessage = "Maximum 50 Characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        [MaxLength(50, ErrorMessage = "Maximum 50 Characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [MaxLength(13, ErrorMessage = "Maximum 13 Characters")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Confirm Password is Required")]
        [MaxLength(13, ErrorMessage = "Maximum 13 Characters")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Gender { get; set; }

        public bool isStudent { get; set; }
        public bool isFullTimeJob { get; set; }
        public bool isPartTimeJob { get; set; }

        [Required(ErrorMessage = "Country is Required")]
        public int CountryID { get; set; }
        
        [Required(ErrorMessage = "City is Required")]
        public int CityID { get; set; }
        
        [MaxLength(250, ErrorMessage = "Maximum 250 Characters")]
        public string AddressDetails { get; set; }

        public List<City> Cities { get; set; }
        public List<Country> Countries { get; set; }

        public int DefaultCountry { get; set; }
    }
}