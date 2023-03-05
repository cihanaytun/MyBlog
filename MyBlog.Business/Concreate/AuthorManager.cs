using MyBlog.Business.Abstract;
using MyBlog.DataAccess.Concreate.EfCore;
using MyBlog.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Business.Concreate
{
    public class AuthorManager : ManagerBase<Author>, IAuthorService
    {
      
    }
}
