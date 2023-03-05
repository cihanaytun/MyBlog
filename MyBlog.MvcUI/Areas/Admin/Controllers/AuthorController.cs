using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Business.Abstract;
using MyBlog.Entities.Concreate;
using MyBlog.MvcUI.Areas.Admin.Models.Author;

namespace MyBlog.MvcUI.Areas.Admin.Controllers
{
    public class AuthorController : BaseController
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;


        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;

        }


        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _authorService.FindAllIncludeAsync(null);
            return View(result.ToList());
        }
        #endregion


        #region Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var authorCreateVM = new AuthorCreateVM();
            return View(authorCreateVM);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(AuthorCreateVM authorVM)
        {
            if (!ModelState.IsValid)
            {
                return View(authorVM);
            }
            var author = _mapper.Map<AuthorCreateVM, Author>(authorVM);

            if (authorVM.ImageUrl == null)
            {
                var extent = Path.GetExtension(authorVM.ImageUrl);
                var randomName = ($"{Guid.NewGuid()}{extent}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Uploads\\Authors", randomName);
                author.ImageUrl = "Uploads\\Authors\\" + randomName;
            }

            var result = await _authorService.CreateAsync(author);
            if (result > 0)
            {
                return RedirectToAction("Index", "Author");
            }
            ModelState.AddModelError("", "Hata");
            @ViewBag.Hata = "Hata";
            return View(authorVM);

        }
        #endregion


        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var author = await _authorService.FindAsync(p => p.Id == id);

            AuthorUpdateVM authorVM = new AuthorUpdateVM
            {
                Name = author.Name,
                Surname= author.Surname,
                UserName = author.UserName,
                Email= author.Email,
                Password= author.Password
               
            };
            return View(authorVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(AuthorUpdateVM authorVM)
        {
            if (!ModelState.IsValid)
            {
                return View(authorVM);
            }

            var author = await _authorService.FindAsync(p => p.Id == authorVM.Id);

        

            author.Id= authorVM.Id;
            author.Name = authorVM.Name;
            author.Surname = authorVM.Surname;
            author.UserName = authorVM.UserName;
            author.Email= authorVM.Email;
            author.Password = authorVM.Password;

            if (authorVM.ImageFile != null)
            {
                var extent = Path.GetExtension(authorVM.ImageFile.FileName);
                var randomName = ($"{Guid.NewGuid()}{extent}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Uploads\\Authors", randomName);
                author.ImageUrl = "Uploads\\Authors\\" + randomName;
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await authorVM.ImageFile.CopyToAsync(stream);
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    authorVM.ImageFile.CopyTo(ms);
                }
            }


            var result = await _authorService.UpdateAsync(author);
            if (result > 0)
            {
                return RedirectToAction("Index", "Author");
            }
            return View(authorVM);

        }
        #endregion


        #region Delete

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var author = await _authorService.FindAsync(p => p.Id == id);
            if (author == null)
            {
                ViewBag.Error = "Yazar bulunamadı";
                return View();
            }
            var result = _authorService.DeleteAsync(author);
            return RedirectToAction("Index");

        }
        #endregion
    }
}
