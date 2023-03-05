using MyBlog.Entities.Concreate;

namespace MyBlog.MvcUI.Areas.Admin.Models.Blog
{
    public class BlogUpdateVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? SubTitle { get; set; }
        public string Content { get; set; }
        public IFormFile? ImageFile { get; set; }
        public bool IsPublish { get; set; }
        public ICollection<CategoryBlog>? Categories { get; set; }
        public Guid AuthorId { get; set; }

    }
}
