using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.DataAccess.EntityConfiguration.Abstract;
using MyBlog.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccess.EntityConfiguration.Concreate
{
    public class CategoryConfiguration<T> : EntityConfigurationBase<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.HasIndex(p => p.Name).IsUnique();

            builder.Property(p => p.IsPublish).HasDefaultValue(false);

            builder.HasOne(p => p.Author)
                .WithMany(p => p.Categories)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
