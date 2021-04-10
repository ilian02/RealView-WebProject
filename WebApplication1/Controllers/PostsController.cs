using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;
using WebSite.Models;
using WebSite.Servises;

namespace WebSite.Controllers
{
    public class PostsController : Controller
    {
        private readonly ILogger<PostsController> _logger;

        public readonly UserManager<IdentityUser> userManager;
        private readonly ICloudinaryService cloudinaryService;

        public readonly IPostServise postServise;

        public PostsController(ILogger<PostsController> logger, IPostServise postServise, UserManager<IdentityUser> userManager, ICloudinaryService cloudinaryService)
        {
            this._logger = logger;
            this.postServise = postServise;
            this.userManager = userManager;
            this.cloudinaryService = cloudinaryService;
            
    }

        [HttpGet]
        public IActionResult AllPosts()
        {
            List <PostViewModel> postModels = postServise.GetAllPosts();
            return View(postModels);
        }

        [HttpGet]
        public IActionResult ProfilePosts(String id)
        {

            List<PostViewModel> posts = postServise.GetAllPosts().Where(x => x.PosterName == id).ToList();
             

            return View(posts);
        }


        [HttpGet]
        public IActionResult GetPostsPageByIndex(int id)
        {
            List<PostViewModel> postModels = postServise.GetAllPosts().Skip(10 * (id - 1)).Take(10).ToList();

            PaginationModel postForPage = new PaginationModel(postModels, 10, id);

            return View(postForPage);
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();
        }

        public IActionResult GetPost(int id)
        {
            PostViewModel post = postServise.GetPost(id);


            return View(post);
        }

        public async Task<IActionResult> CreatePost(PostModel postModel)
        {

            var user = await userManager.GetUserAsync(HttpContext.User);
            postModel.PosterName = user.UserName;

            bool result = await this.postServise.CreatePost(postModel);

            return Redirect("/");
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateComment()
        {
            return View();
        }

        public async Task<IActionResult> CreateComment(CommentModel commentModel)
        {
            bool result = await this.postServise.CreateComment(commentModel);

            return Redirect("/");
        }

        public PartialViewResult GetComments(int id)
        {
            List<CommentModel> comments = postServise.GetPostComments(id);

            return PartialView("GetComments", comments);
        }
    }
}
