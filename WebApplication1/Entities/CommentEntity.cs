using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Entities
{
    public class CommentEntity
    {
        [Key]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public String PosterName { get; set; }
        public String Description { get; set; }
        public int Stars { get; set; }
        public int PostID { get; set; }
    }
}
