namespace MyBlog.MvcUI.Areas.Admin.Models.Category
{
    public class CategoryUpdateVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsPublish { get; set; }
    }
}
