using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogProject.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        [MaxLength(50, ErrorMessage = "Maximum 50 Characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [MaxLength(13, ErrorMessage = "Maximum 13 Characters")]
        public string Password { get; set; }
    }
}