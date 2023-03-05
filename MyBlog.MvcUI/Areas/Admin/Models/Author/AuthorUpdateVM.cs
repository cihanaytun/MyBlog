namespace MyBlog.MvcUI.Areas.Admin.Models.Author
{
    public class AuthorUpdateVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
