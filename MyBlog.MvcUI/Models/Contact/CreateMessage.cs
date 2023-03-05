using System.Reflection.Metadata.Ecma335;

namespace MyBlog.MvcUI.Models.Contact
{
    public class CreateMessage
    {
        public string NameSurname { get; set; }
        public string Message { get; set; }
        public string FromEmail { get; set; }

    }
}
