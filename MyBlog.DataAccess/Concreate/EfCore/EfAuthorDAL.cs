using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concreate;

namespace MyBlog.DataAccess.Concreate.EfCore
{
    public class EfAuthorDAL : EfRepositoryBase<Author>, IAuthorDAL
    {

    }
}
