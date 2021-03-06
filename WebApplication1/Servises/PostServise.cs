using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebSite.Models;

namespace WebSite.Servises
{
    public class PostServise : IPostServise
    {
        private readonly ICloudinaryService cloudinaryService;
        private readonly ApplicationDbContext AppDbContext;
        public readonly UserManager<IdentityUser> userManager;

        public PostServise(ApplicationDbContext appDbContext, UserManager<IdentityUser> userManager, ICloudinaryService cloudinaryService)
        {
            this.AppDbContext = appDbContext;
            this.userManager = userManager;
            this.cloudinaryService = cloudinaryService;
        }

        [HttpGet]
        [Route("Posts/GetPost/{id:int}")]
        public PostViewModel GetPost([FromQuery (Name = "id")] int id = 1)
        {
            PostEntity postEntity = AppDbContext.Posts.Find(id);

            PostViewModel result = new PostViewModel
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

        public List<PostViewModel> GetAllPosts()
        {
            var entities = AppDbContext.Posts.OrderByDescending(c => c.Date).ToList();

            List<PostViewModel> result = new List<PostViewModel>();

            foreach (PostEntity post in entities)
            {
                PostViewModel postModel = new PostViewModel
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

            string url = await this.cloudinaryService.UploadImage(postModel.FileUpload);


            PostEntity postEntity = new PostEntity
            {
                Title = postModel.Title,
                PosterName = postModel.PosterName,
                ImageURL = url,
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
