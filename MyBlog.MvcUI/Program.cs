using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MyBlog.API.AutoMapper;
using MyBlog.DataAccess.Context;
using MyBlog.MvcUI.Extensions;

namespace MyBlog.MvcUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //db context
            builder.Services.AddDbContext<MyBlogContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //automapper
            builder.Services.AddAutoMapper(typeof(MapperConfig));


            //extensions
            builder.Services.AddMyBlogExtensions();



            #region Cookie Base Authentication Settings
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                            .AddCookie(p =>
                            {
                                p.LoginPath = "/Login/Login";
                                p.LogoutPath = "/Login/LogOut";
                                p.Cookie.Name = "MyBlogCookie";
                                p.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                            });
            #endregion


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // added
            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"

                );
            });

            app.MapControllerRoute(
                 name: "default",
                 pattern: "{controller=Home}/{action=Index}/{id?}");





            #region URL
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "blog",
            //        template: "{title}/{id}",
            //        defaults: new { controller = "News", action = "Detail" });

            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=News}/{action=Index}/{id?}");
            //});


            //Html
            //< a href = "/@Url.FriendlyUrl(news.Title)/@news.ID" target = "_blank" style = "text-decoration: none;" >
            //    https://www.borakasmer.com/net-core-mvcde-bir-haber-basligini-urle-koyma/

            #endregion


            app.Run();
        }
    }
}