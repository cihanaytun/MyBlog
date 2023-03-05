using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Business.Abstract;
using MyBlog.DataAccess.Context;
using MyBlog.MvcUI.Models;
using MyBlog.MvcUI.Models.Contact;
using System.Diagnostics;
using MyBlog.Entities.Concreate;
using X.PagedList;
using System.Net.Mail;
using System.Web.Helpers;
using MyBlog.MvcUI.Areas.Admin.Models.Blog;

namespace MyBlog.MvcUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogService _blogService;
        private readonly MyBlogContext _myBlogContext;
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;
        private readonly IUrlHelper _urlHelper;

        public HomeController(ILogger<HomeController> logger, IBlogService blogService, MyBlogContext myBlogContext, IAuthorService authorService, IMapper mapper)
        {
            _logger = logger;
            _blogService = blogService;
            _myBlogContext = myBlogContext;
            _authorService = authorService;
            _mapper = mapper;
        }


        #region Index
        public async Task<IActionResult> Index(int? page)
        {
            var result = _blogService.FindAllIncludeAsync(null, p => p.Categories, p => p.Author).Result
                .Where(p => p.IsPublish == true)
                                .OrderByDescending(p => p.CreatedDate)

                .ToList();
            int pageNumber = page ?? 1;

            return View(result.ToPagedList<Blog>(pageNumber, 2));
        }


        #endregion

        [HttpGet]
        public async Task<IActionResult> Post(Guid id)
        {
            var result = await _blogService.GetByIdAsync(id.ToString());
            result.Author = await _myBlogContext.Author.FindAsync(result.AuthorId);
            result.ImagePath = "/" + result.ImagePath.Replace("\\", "/");
            return View(result);
        }



        [HttpGet]
        public async Task<IActionResult> Contact()
        {
            CreateMessage createMessage = new CreateMessage();

            return View(createMessage);
        }

        [HttpPost]
        public async Task<IActionResult> Contact(CreateMessage createMessage)
        {
                 return View(createMessage);
        }




        [HttpGet]
        public async Task<IActionResult> About()
        {
            var result = await _authorService.FindAsync(null);
            result.ImageUrl = "/" + result.ImageUrl.Replace("\\", "/");
            return View(result);
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}