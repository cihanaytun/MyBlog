using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Business.Abstract;
using MyBlog.DataAccess.Context;
using MyBlog.MvcUI.Models.Login;
using System.Security.Claims;

namespace MyBlog.MvcUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IMapper _mapper;

        public LoginController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }


        #region Login
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            LoginVM loginVM = new LoginVM();
            return View(loginVM);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            var user = _authorService.FindAsync(p => p.Email == loginVM.Email && p.Password == loginVM.Password).Result;
            if (user == null)
            {
                return View(loginVM);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.Name,user.Name),
                new Claim("SurName",user.Surname),
                new Claim(ClaimTypes.Uri,user.ImageUrl==null?"":user.ImageUrl),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authenticationProperty = new AuthenticationProperties
            {
                IsPersistent = loginVM.RememberMe
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), authenticationProperty);

            return RedirectToAction("Index", "Home", new { Area = "Admin" });
        }
        #endregion


        #region Logout
        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        } 
        #endregion
    }
}
