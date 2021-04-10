using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Models
{
    public class PostModel
    {
        [Key]
        public int PostID { get; set; }
        public String Title { get; set; }
        public DateTime Date { get; set; }
        public String PosterName { get; set; }
        public String Description { get; set; }
        
        public int Stars { get; set; }      
        public IFormFile FileUpload { get; set; }
    }
}
