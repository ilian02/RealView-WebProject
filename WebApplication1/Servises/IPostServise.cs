using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;
using WebSite.Models;

namespace WebSite.Servises
{
    public interface IPostServise
    {
        Task<bool> CreatePost(PostModel postModel);
        Task<bool> CreateComment(CommentModel commentModel);
        public List<PostModel> GetAllPosts();
        public PostModel GetPost(int postId);
        public List<CommentModel> GetPostComments(int id);
    }
}
