using MyBlog.Business.Abstract;
using MyBlog.Entities.Concreate;

namespace MyBlog.Business.Concreate
{
    public class BlogManager : ManagerBase<Blog>, IBlogService
    {

    }
}
