using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebSite.Models;

namespace WebSite.Servises
{
    public class PostServise : IPostServise
    {
        private readonly ApplicationDbContext AppDbContext;

        public PostServise(ApplicationDbContext appDbContext)
        {
            this.AppDbContext = appDbContext;
        }

        [HttpGet]
        [Route("Posts/GetPost/{id:int}")]
        public PostModel GetPost([FromQuery (Name = "id")] int id = 1)
        {
            PostEntity postEntity = AppDbContext.Posts.Find(id);

            PostModel result = new PostModel
            {
                    PostID = postEntity.ID,
                    Title = postEntity.Title,
                    PosterName = postEntity.PosterName,
                    Description = postEntity.Description,
                    Date = postEntity.Date,
                    Stars = postEntity.Stars,
                    ImageURL = postEntity.ImageURL
            };

            return result;
        }

        public List<PostModel> GetAllPosts()
        {
            var entities = AppDbContext.Posts.OrderByDescending(c => c.Date).ToList();

            List<PostModel> result = new List<PostModel>();

            foreach (PostEntity post in entities)
            {
                PostModel postModel = new PostModel
                {
                    PostID = post.ID,
                    Title = post.Title,
                    PosterName = post.PosterName,
                    Description = post.Description,
                    Date = post.Date,
                    Stars = post.Stars,
                    ImageURL = post.ImageURL
                };

                result.Add(postModel);
            }

            return result;
        }

        [HttpPost]
        public async Task<bool> CreatePost(PostModel postModel)
        { 
            PostEntity postEntity = new PostEntity
            {
                Title = postModel.Title,
                PosterName = postModel.PosterName,
                ImageURL = postModel.ImageURL,
                Description = postModel.Description,
                Stars = postModel.Stars,
                Date = DateTime.Now
            };


            bool result = await this.AppDbContext.AddAsync(postEntity) != null;

            await this.AppDbContext.SaveChangesAsync();

            return result;
        }

        public async Task<bool> CreateComment(CommentModel commentModel)
        {
            CommentEntity commentEntity = new CommentEntity
            {
                PostID = commentModel.PostID,
                Description = commentModel.Description,
                PosterName = commentModel.PosterName,
                Stars = commentModel.Stars,
                Date = DateTime.Now
            };


            bool result = await this.AppDbContext.AddAsync(commentEntity) != null;

            await this.AppDbContext.SaveChangesAsync();

            return result;
        }


        public List<CommentModel> GetPostComments(int id)
        {
            var entities = AppDbContext.Comments.Where(c => c.PostID == id).OrderByDescending(c => c.Date).ToList();

            List<CommentModel> result = new List<CommentModel>();

            foreach (CommentEntity comment in entities)
            {
                CommentModel commentModel = new CommentModel
                {
                    ID = comment.ID,
                    PosterName = comment.PosterName,
                    Description = comment.Description,
                    PostID = comment.PostID,
                    Stars = comment.Stars
                    
                };

                result.Add(commentModel);
            }

            return result;
        }
    }
}
