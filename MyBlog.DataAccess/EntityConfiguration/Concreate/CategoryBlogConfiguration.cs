using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Entities.Concreate;

namespace MyBlog.DataAccess.EntityConfiguration.Concreate
{
    public class CategoryBlogConfiguration : IEntityTypeConfiguration<CategoryBlog>
    {
        public void Configure(EntityTypeBuilder<CategoryBlog> builder)
        {
            builder.HasKey(p => new { p.CategoryId, p.BlogId });


            builder.HasOne(p => p.Category)
                .WithMany(p => p.Blogs)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(p => p.Blog)
                .WithMany(p => p.Categories)
                .HasForeignKey(p => p.BlogId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
