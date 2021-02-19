using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Entities
{
    public class PostEntity
    {
        [Key]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        [Required]
        public String Title { get; set; }
        [Required]
        public String ImageURL { get; set; }
        public String PosterName { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]
        public int Stars { get; set; }
    }
}
