using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.DataAccess.Concreate.EfCore
{
    public class EfRoleDAL : EfRepositoryBase<Role>,IRoleDAL
    {
    }
}
