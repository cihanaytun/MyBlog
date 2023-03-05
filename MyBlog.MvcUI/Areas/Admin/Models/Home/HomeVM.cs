
using System.Collections;

namespace MyBlog.MvcUI.Areas.Admin.Models.Home
{
    public class HomeVM
    {
        public int TotalNumbereOfBlogs { get; set; }
        public int NumberOfBlogsPublished { get; set; }
        public int NumberOfUnpublishedBlogs { get; set; }

        public ICollection<MyBlog.Entities.Concreate.Blog>? Blogs { get; set; }



    }
}
