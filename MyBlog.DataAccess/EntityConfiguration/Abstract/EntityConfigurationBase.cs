using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccess.EntityConfiguration.Abstract
{
    public class EntityConfigurationBase<T> : IEntityTypeConfiguration<T> where T : BaseEntity, new()
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.CreatedDate).HasDefaultValue(new DateTime());
        }

    }
}
