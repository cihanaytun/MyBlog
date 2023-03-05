using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Business.Abstract;
using MyBlog.Entities.Concreate;
using MyBlog.MvcUI.Areas.Admin.Models.Category;
using System.Security.Claims;

namespace MyBlog.MvcUI.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }


        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = _categoryService.FindAllIncludeAsync(null, p => p.Author).Result.ToList();

            return View(result);
        }
        #endregion


        #region Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categoryVM = new CategoryCreateVM();
            return View(categoryVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryVM);
            }

            var category = _mapper.Map<CategoryCreateVM, Category>(categoryVM);
            category.AuthorId = Guid.Parse(User.Claims.FirstOrDefault(p => p.Type == ClaimTypes.NameIdentifier).Value);
            var result = await _categoryService.CreateAsync(category);
            if (result > 0)
            {
                return RedirectToAction("Index", "Category");
            }
            return View(categoryVM);
        }
        #endregion


        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var category = await _categoryService.FindAsync(p => p.Id == id);
            CategoryUpdateVM categoryVM = new CategoryUpdateVM
            {
                Name = category.Name,
                IsPublish = category.IsPublish
            };

            return View(categoryVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(CategoryUpdateVM categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryVM);
            }

            var category = await _categoryService.FindAsync(p => p.Id == categoryVM.Id);
            category.Name = categoryVM.Name;
            category.Id = categoryVM.Id;
            category.IsPublish = categoryVM.IsPublish;

            var result = await _categoryService.UpdateAsync(category);
            if (result > 0)
            {
                return RedirectToAction("Index", "Category");
            }
            return View(categoryVM);

        }
        #endregion


        #region Delete

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _categoryService.FindAsync(p => p.Id == id);
            if (category == null)
            {
                ViewBag.Error = "Kategori bulunamadı";
                return View();
            }
            var result = _categoryService.DeleteAsync(category);
            return RedirectToAction("Index");

        }
        #endregion
    }
}
