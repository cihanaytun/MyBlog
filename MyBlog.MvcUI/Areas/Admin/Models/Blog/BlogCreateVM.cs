using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyBlog.MvcUI.Areas.Admin.Models.Blog
{
    public class BlogCreateVM
    {
        public string Title { get; set; }
        public string? SubTitle { get; set; }
        public string Content { get; set; }
        public IFormFile? ImageFile { get; set; }
        public bool IsPublish { get; set; }
        public IList<SelectListItem>? Categories { get; set; }
        public IList<Guid>? CategoryId { get; set; }
        public Guid AuthorId { get; set; }

    }
}
