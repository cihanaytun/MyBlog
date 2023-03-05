using MyBlog.Business.Abstract;
using MyBlog.Business.Concreate;

namespace MyBlog.MvcUI.Extensions
{
    public static class MyBlogExtensions
    {
        public static IServiceCollection AddMyBlogExtensions(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IBlogService, BlogManager>();
            services.AddScoped<IAuthorService, AuthorManager>();
            services.AddScoped<IRoleService, RoleManager>();


            return services;
        }
    }
}
