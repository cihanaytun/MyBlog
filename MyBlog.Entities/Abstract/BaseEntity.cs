using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entities.Abstract
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
