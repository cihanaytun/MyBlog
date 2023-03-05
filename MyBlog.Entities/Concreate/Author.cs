using MyBlog.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entities.Concreate
{
    public class Author : BaseEntity
    {
        public Author()
        {
            Blogs = new HashSet<Blog>();
            Categories = new HashSet<Category>();
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? ImageUrl { get; set; }

        public Role Role { get; set; }

        public ICollection<Blog>? Blogs { get; set; }
        public ICollection<Category>? Categories { get; set; }


    }
}
