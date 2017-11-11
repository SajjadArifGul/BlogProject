using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogProject.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [MaxLength(50, ErrorMessage ="Maximum 50 Characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        [MaxLength(50, ErrorMessage = "Maximum 50 Characters")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password is Required")]
        [MaxLength(13, ErrorMessage = "Maximum 13 Characters")]
        public string Password { get; set; }
        
        public string Gender { get; set; }

        public bool isStudent { get; set; }
        public bool isFullTimeJob { get; set; }
        public bool isPartTimeJob { get; set; }
        
        [Required(ErrorMessage = "City is Required")]
        public virtual City City { get; set; }
        
        [MaxLength(250, ErrorMessage = "Maximum 250 Characters")]
        public string AddressDetails { get; set; }
    }
}