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
    public class BlogConfiguration<T> : EntityConfigurationBase<Blog>
    {
        public override void Configure(EntityTypeBuilder<Blog> builder)
        {
            base.Configure(builder);

            
            builder.HasOne(p => p.Author)
                .WithMany(p => p.Blogs)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(p => p.IsPublish).HasDefaultValue(false);


        }
    }
}
