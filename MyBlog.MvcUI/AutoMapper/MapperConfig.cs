using AutoMapper;
using MyBlog.Entities.Concreate;
using MyBlog.MvcUI.Areas.Admin.Models.Author;
using MyBlog.MvcUI.Areas.Admin.Models.Blog;
using MyBlog.MvcUI.Areas.Admin.Models.Category;
using MyBlog.MvcUI.Models.Login;

namespace MyBlog.API.AutoMapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            #region Admin Side
            CreateMap<CategoryCreateVM, Category>().ReverseMap();
            CreateMap<CategoryUpdateVM, Category>().ReverseMap();

            CreateMap<AuthorCreateVM,Author>().ReverseMap();
            CreateMap<AuthorUpdateVM, Author>().ReverseMap();

            CreateMap<BlogCreateVM,Blog>().ReverseMap();
            CreateMap<BlogUpdateVM, Blog>().ReverseMap();




            #endregion


            #region UserSide
            CreateMap<LoginVM, Author>().ReverseMap();

            #endregion



        }
    }
}
