using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebSite.Models;
using WebSite.Servises;

namespace WebSite.ViewComponents
{
    [ViewComponent(Name = "getComments")] 
    public class getComments: ViewComponent
    {
        public readonly IPostServise postServise;

        public getComments(IPostServise postServise)
        {
            this.postServise = postServise;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var result = GetCommentModels(id);
            return View("getComments", result);
        }

        public List<CommentModel> GetCommentModels(int id)
        {
            List<CommentModel> result =  postServise.GetPostComments(id).ToList();
            return result;        
        }

    }
}
