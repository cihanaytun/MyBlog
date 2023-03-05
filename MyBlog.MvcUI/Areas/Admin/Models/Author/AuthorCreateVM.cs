namespace MyBlog.MvcUI.Areas.Admin.Models.Author
{
    public class AuthorCreateVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }

        public string? ImageUrl { get; set; }
    }
}
