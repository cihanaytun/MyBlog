using MyBlog.Entities.Abstract;

namespace MyBlog.Entities.Concreate
{
    public class Blog : BaseEntity
    {
        public Blog()
        {
            Categories = new HashSet<CategoryBlog>();
        }

        public string Title { get; set; }
        public string? SubTitle { get; set; }
        public string Content { get; set; }
        public string? ImagePath { get; set; }
        public bool IsPublish { get; set; }
        public DateTime? PublishDate { get; set; }

        public ICollection<CategoryBlog>? Categories { get; set; }

        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
