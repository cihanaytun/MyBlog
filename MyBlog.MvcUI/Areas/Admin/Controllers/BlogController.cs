using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyBlog.Business.Abstract;
using MyBlog.DataAccess.Context;
using MyBlog.Entities.Concreate;
using MyBlog.MvcUI.Areas.Admin.Models.Blog;
using System.Security.Claims;

namespace MyBlog.MvcUI.Areas.Admin.Controllers
{
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;
        private readonly IMapper _mapper;
        private readonly MyBlogContext _myBlogContext;
        private readonly ICategoryService _categoryService;

        public BlogController(IBlogService blogService, IMapper mapper, MyBlogContext myBlogContext, ICategoryService categoryService)
        {
            _blogService = blogService;
            _mapper = mapper;
            _myBlogContext = myBlogContext;
            _categoryService = categoryService;
        }


        #region Index
        public async Task<IActionResult> Index()
        {
            var authorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = _blogService.FindAllIncludeAsync(p => p.AuthorId == authorId, p => p.Categories).Result.ToList();
            var resultt = _blogService.FindAllAsnyc(p => p.AuthorId == authorId).Result.ToList();
            return View(resultt);
        }
        #endregion


        #region Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            BlogCreateVM blogCreateVM = new BlogCreateVM();
            blogCreateVM.Categories = await GetCategories();
            return View(blogCreateVM);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM blogCreateVM)
        {
            if (!ModelState.IsValid)
            {
                @ViewBag.Categories = await GetCategories();

                return View(blogCreateVM);
            }

            var blog = _mapper.Map<BlogCreateVM, Blog>(blogCreateVM);
            blog.AuthorId = Guid.Parse(User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value);
            if (blog.IsPublish == true)
            {
                blog.PublishDate = DateTime.Now;
            }


            if (blogCreateVM.ImageFile != null)
            {
                var extent = Path.GetExtension(blogCreateVM.ImageFile.FileName);
                var randomName = ($"{Guid.NewGuid()}{extent}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Uploads\\Blogs", randomName);
                blog.ImagePath = "Uploads\\Blogs\\" + randomName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await blogCreateVM.ImageFile.CopyToAsync(stream);
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    blogCreateVM.ImageFile.CopyTo(ms);
                }
            }

            var result = await _blogService.CreateAsync(blog);


            if (Convert.ToUInt64(blogCreateVM.CategoryId) > 0)
            {
                foreach (var item in blogCreateVM.CategoryId)
                {
                    var category = await _categoryService.FindAsync(p => p.Id == item);

                    await _blogService._myBlogContext.CategoryBlogs.AddAsync(new CategoryBlog
                    {
                        BlogId = blog.Id,
                        CategoryId = category.Id
                    });
                    await _blogService._myBlogContext.SaveChangesAsync();
                }
            }

            if (result > 0)
            {
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                ModelState.AddModelError("", "Bilinmeyen bir hata olustu. Daha Sonra Tekrar Deneyiniz");
                @ViewBag.Categories = await GetCategories();
                return View(blogCreateVM);
            }

        }


        #endregion


        #region Update

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var blog = await _blogService.FindAsync(p => p.Id == id);

            BlogUpdateVM updateVM = new BlogUpdateVM
            {
                Title = blog.Title,
                SubTitle = blog.SubTitle,
                Content = blog.Content,
                IsPublish = blog.IsPublish
            };
            ViewBag.Categories = _myBlogContext.Categories.Where(p => p.IsPublish == true).Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString()
            }).ToList();
            return View(updateVM);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(BlogUpdateVM blogUpdateVM)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _myBlogContext.Categories.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                }).ToList();
                return View(blogUpdateVM);
            }


            var blog = await _blogService.FindAsync(p => p.Id == blogUpdateVM.Id);
            if (blogUpdateVM.ImageFile != null)
            {
                var extent = Path.GetExtension(blogUpdateVM.ImageFile.FileName);
                var randomName = ($"{Guid.NewGuid()}{extent}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Uploads\\Blogs", randomName);
                blog.ImagePath = "Uploads\\Blogs\\" + randomName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await blogUpdateVM.ImageFile.CopyToAsync(stream);
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    blogUpdateVM.ImageFile.CopyTo(ms);
                }
            }


            blog.Title = blogUpdateVM.Title;
            blog.SubTitle = blogUpdateVM.SubTitle;
            blog.Content = blogUpdateVM.Content;
            blog.AuthorId = Guid.Parse(User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value);
            blog.IsPublish = blogUpdateVM.IsPublish;
            if (blog.IsPublish == true)
            {
                blog.PublishDate = DateTime.Now;
            }
            else
            {
                blog.PublishDate = null;
            }


            var result = await _blogService.UpdateAsync(blog);

            if (result > 0)
            {
                return RedirectToAction("Index", "Blog");
            }
            else
            {
                ModelState.AddModelError("", "Bilinmeyen bir hata olustu. Daha Sonra Tekrar Deneyiniz");
                ViewBag.Categories = _myBlogContext.Categories.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Id.ToString()
                }).ToList();
                return View(blogUpdateVM);
            }

        }


        #endregion


        #region Delete

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var blog = await _blogService.FindAsync(p => p.Id == id);
            if (blog == null)
            {
                ViewBag.Error = "Blog bulunamadı";
                return View();
            }
            var result = _blogService.DeleteAsync(blog);
            return RedirectToAction("Index");

        }
        #endregion


        #region GetCategories

        [NonAction]
        private async Task<List<SelectListItem>> GetCategories()
        {
            var categoryList = new List<SelectListItem>();
            var categories = await _categoryService.FindAllAsnyc(p => p.IsPublish == true);

            foreach (var item in categories)
            {
                SelectListItem listItem = new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                };
                categoryList.Add(listItem);
            }
            return categoryList;
        }

        #endregion

    }
}
