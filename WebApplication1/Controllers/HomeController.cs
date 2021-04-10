using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebSite.Models;
using WebSite.Servises;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public readonly IPostServise postServise;

        private readonly UserManager<IdentityUser> userManager;
        public HomeController(ILogger<HomeController> logger, IPostServise postServise, UserManager<IdentityUser> userManager )
        {
            this.postServise = postServise;
            this.userManager = userManager;

            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Profile(string id)
        {
            List<PostViewModel> posts = postServise.GetAllPosts().Where(x => x.PosterName == id).ToList();


            return View(posts);
        }
        public async Task<IActionResult> UpdateEmail(string email, string id)
        {
            IdentityUser loginUser = await userManager.FindByNameAsync(id);
            loginUser.Email = email;
            await userManager.UpdateAsync(loginUser);

            return RedirectToAction(nameof(Profile), new { id = id }) ;
        }
    }
}
