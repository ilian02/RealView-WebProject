using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Models;
using WebSite.Servises;

namespace WebSite.ViewComponents
{
    [ViewComponent(Name = "createNewComment")] 
    public class createNewComment: ViewComponent
    {
        public readonly UserManager<IdentityUser> userManager;

        public createNewComment(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int postID)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            CommentModel comment = new CommentModel { PostID = postID, PosterName = user.UserName };
            return View("createNewComment", comment);
        }

    }
}
