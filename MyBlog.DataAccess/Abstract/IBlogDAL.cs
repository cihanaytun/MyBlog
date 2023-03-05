using MyBlog.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccess.Abstract
{
    public interface IBlogDAL : IRepositoryBase<Blog>
    {
    }
}
