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
    public class AuthorConfiguration<T> : EntityConfigurationBase<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            base.Configure(builder);

            builder.HasMany(p => p.Blogs)
                .WithOne(p => p.Author)
                .HasForeignKey(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(p => p.Categories)
              .WithOne(p => p.Author)
              .HasForeignKey(p => p.AuthorId)
              .OnDelete(DeleteBehavior.Restrict);



            builder.HasOne(p => p.Role)
                .WithOne(p => p.Author)
                .HasForeignKey<Role>(p => p.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Surname).IsRequired();
            builder.Property(p => p.Password).IsRequired();
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.UserName).IsRequired();



            // Base Data
            builder.HasData(new Author
            {
                Id = Guid.Parse("5d1b7711-d212-4e99-ba0c-0afa34b7e3e0"),
                Name = "Ali",
                Surname = "Deneme",
                UserName = "ali",
                Password = "123",
                Email = "ali@gmail.com",
                CreatedDate = DateTime.Now
                //RoleId = Guid.Parse("4d1b7711-d212-4e99-ba0c-0afa34b7e3e0")

            });




        }
    }
}
