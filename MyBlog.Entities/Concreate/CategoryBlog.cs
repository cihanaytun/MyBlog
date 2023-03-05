using MyBlog.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entities.Concreate
{
    public class CategoryBlog 
    {
        public Guid CategoryId { get; set; }
        public Guid BlogId { get; set; }

        public Category Category { get; set; }
        public Blog Blog { get; set; }


    }
}
