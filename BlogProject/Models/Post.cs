using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BlogProject.Models
{
    public class Post
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(300, ErrorMessage ="Max Title Length is 100 Characters"), MinLength(10, ErrorMessage = "Min Title Length is 10 Characters")]
        public string Title { get; set; }

        [MaxLength(500, ErrorMessage = "Max Length is 200 Characters")]
        public string Summary { get; set; }

        [Required]
        public string Description { get; set; }

        [NotMapped]
        public string PostSummary {
            get {
                if(!string.IsNullOrEmpty(Summary))
                {
                    return Summary;
                }
                else
                {
                    var descriptionSummary = Description.Length > 40 ? Description.Substring(0, 40) : Description;

                    return string.Format("{0}{1}", descriptionSummary, "...");
                }
            }
        }

        [NotMapped]
        public string URL
        {
            get
            {
                var url = string.Empty;
                if (Title.Length < 30)
                {
                    url = Title.Replace(' ', '-').Replace('.', '-');
                }
                else
                {
                    url = Title.Substring(0, 30).Replace(' ', '-').Replace('.', '-');
                }
                
                //remove dash / dot if it is the last character in url
                if (url[url.Length-1] == '-' || url[url.Length - 1] == '.') url = url.Remove(url.Length - 1, 1);

                return url.ToLower();
            }
        }

        [Required]
        public DateTime PublishedTime { get; set; }

        [Required]
        public virtual User Author { get; set; }
        
        public virtual Image Image { get; set; }
    }
}