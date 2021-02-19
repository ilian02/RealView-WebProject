using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Models
{
    public class CommentModel
    {
        [Key]
        public int ID { get; set; }
        public String PosterName { get; set; }
        public String Description { get; set; }
        public int Stars { get; set; }
        public int PostID { get; set; }
    }
}


