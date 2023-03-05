using MyBlog.Entities.Abstract;

namespace MyBlog.Entities.Concreate
{
    public class Category : BaseEntity
    {
        public Category()
        {
            Blogs = new HashSet<CategoryBlog>();
        }

        public string Name { get; set; }
        public bool IsPublish { get; set; }


        public ICollection<CategoryBlog>? Blogs { get; set; }

        public Guid AuthorId { get; set; }
        public Author? Author { get; set; }

    }
}