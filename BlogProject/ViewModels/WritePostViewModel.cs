using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogProject.ViewModels
{
    public class WritePostViewModel
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Max Title Length is 100 Characters"), MinLength(10, ErrorMessage = "Min Title Length is 10 Characters")]
        public string Title { get; set; }

        [MaxLength(100, ErrorMessage = "Max Length is 100 Characters")]
        public string Summary { get; set; }

        [Required]
        public string Description { get; set; }

        public int ImageID { get; set; }
    }
}