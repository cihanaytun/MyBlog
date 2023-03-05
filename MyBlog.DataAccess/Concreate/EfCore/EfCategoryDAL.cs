using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concreate;

namespace MyBlog.DataAccess.Concreate.EfCore
{
    public class EfCategoryDAL : EfRepositoryBase<Category>, ICategoryDAL
    {

    }
}
