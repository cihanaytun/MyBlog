using MyBlog.DataAccess.Abstract;
using MyBlog.Entities.Concreate;

namespace MyBlog.DataAccess.Concreate.EfCore
{
    public class EfBlogDAL : EfRepositoryBase<Blog>, IBlogDAL
    {

    }
}
