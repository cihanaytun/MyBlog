using MyBlog.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Business.Abstract
{
    public interface ICategoryService : IManagerService<Category>
    {
    }
}
