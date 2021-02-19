using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSite.Models
{
    public class PaginationModel
    {

        public List<PostModel> posts { get; set; }
        public int size { get; set; }
        public int index { get; set; }

        public PaginationModel(List<PostModel> postModels, int size, int index)
        {
            this.posts = postModels;
            this.size = size;
            this.index = index;
        }
    }
}
