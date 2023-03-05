using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Business.Abstract;
using MyBlog.MvcUI.Areas.Admin.Models.Home;
using System.Globalization;
using System.Security.Claims;

namespace MyBlog.MvcUI.Areas.Admin.Controllers
{

    public class HomeController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;

        public HomeController(IBlogService blogService, ICategoryService categoryService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
        }


        public async Task<IActionResult> Index()
        {
            var author = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var lastfiveBlogs = _blogService.FindAllAsnyc(p => p.AuthorId == author).Result.Take(5).OrderByDescending(p => p.CreatedDate).ToList();

            var numberOfBlogsPublished = _blogService.FindAllAsnyc(p => p.AuthorId == author).Result.Count();

            var totalNumbereOfBlogs = _blogService.FindAllAsnyc(p => p.AuthorId == author).Result.Where(p => p.IsPublish == true).Count();

            var numberOfUnpublishedBlogs = _blogService.FindAllAsnyc(p => p.AuthorId == author).Result.Where(p => p.IsPublish == false).Count();



            HomeVM homeVM = new HomeVM
            {
                Blogs = lastfiveBlogs,
                NumberOfBlogsPublished = numberOfBlogsPublished,
                TotalNumbereOfBlogs = totalNumbereOfBlogs,
                NumberOfUnpublishedBlogs = numberOfUnpublishedBlogs
            };


            return View(homeVM);
        }
    }
}
