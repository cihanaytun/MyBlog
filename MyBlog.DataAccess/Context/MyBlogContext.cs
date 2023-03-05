using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyBlog.Entities.Concreate;
using System.Reflection;

namespace MyBlog.DataAccess.Context
{
    public class MyBlogContext : DbContext
    {
        public DbSet<Author> Author { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<CategoryBlog> CategoryBlogs { get; set; }




        public MyBlogContext()
        {

        }
        public MyBlogContext(DbContextOptions<MyBlogContext> options) : base(options)
        {

        }


        private static string ConnectionString = null;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                if (ConnectionString == null)
                {
                    string configFileName = "appsettings.json";
                    var configuration = new ConfigurationBuilder()
                        //.SetBasePath(Directory.GetCurrentDirectory())
                        .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../MyBlog.MvcUI"))
                        //.SetBasePath(Directory.GetCurrentDirectory(),"../Cinema")
                        //.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                        //.SetBasePath(Package.Current.InstalledLocation.Path)
                        //.AddJsonFile(configFileName, true, true)

                        .AddJsonFile(configFileName, false)

                       .Build();
                    ConnectionString = configuration.GetConnectionString("DefaultConnection");
                }
                optionsBuilder.UseSqlServer(ConnectionString);


            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
    }
}
