using MyBlog.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entities.Concreate
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }


        public Guid AuthorId { get; set; }
        public Author Author { get; set; }

    }
}
