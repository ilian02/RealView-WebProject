using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Linq;
using WebApplication1.Controllers;
using WebApplication1.Data;
using WebSite.Controllers;
using WebSite.Models;
using WebSite.Servises;

namespace NUnitTesting
{
    public class Tests
    {
        IPostServise postServise;
        ICloudinaryService cloudinaryService;
        ApplicationDbContext AppDbContext;
        UserManager<IdentityUser> userManager;
        public IConfiguration Configuration;

        [SetUp]
        public void Setup()
        {    
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                 .UseInMemoryDatabase($"TESTS-DB-{Guid.NewGuid().ToString()}")
                 .Options;

            this.AppDbContext = new ApplicationDbContext(options);
        }

        [Test]
        public void CreatePostTest()
        {
            this.postServise = new PostServise(this.AppDbContext, userManager, cloudinaryService);

            PostModel postModel = new PostModel() { Description = "Temp", PosterName = "PosterName", Stars = 5, Title = "current title", Date = DateTime.Now };

            postServise.CreatePost(postModel);
            PostViewModel pvm = postServise.GetAllPosts().FirstOrDefault();
            Assert.AreEqual("PosterName", pvm.PosterName, "Poster Name is not the same");
        }
    }
}