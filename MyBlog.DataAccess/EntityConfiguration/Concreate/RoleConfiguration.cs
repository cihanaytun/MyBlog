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
    public class RoleConfiguration<T> : EntityConfigurationBase<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.Name).IsRequired();
            builder.HasIndex(p => p.Name).IsUnique();


            //builder.HasOne(p => p.Author)
            //    .WithOne(p => p.Role)
            //    .HasForeignKey<Role>(p => p.AuthorId)
            //    .OnDelete(DeleteBehavior.Restrict);


            builder.HasData(new Role
            {
                Id = Guid.Parse("4d1b7711-d212-4e99-ba0c-0afa34b7e3e0"),
                Name = "Admin",
                CreatedDate = DateTime.Now,
                AuthorId = Guid.Parse("5d1b7711-d212-4e99-ba0c-0afa34b7e3e0"),
            });
        }
    }
}
