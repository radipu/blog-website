using System.ComponentModel.DataAnnotations;

namespace My_Blog_Website.Models
{
    public class Contacts
    {
        [Key]
        public int MsgId { get; set; }
        
        [Required(ErrorMessage = "How should I address you?")]
        public string Name { get; set; }

        [Required(ErrorMessage = "What is your email address?")]
        public string Email { get; set; }

        [Required(ErrorMessage = "What is the subject?")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "What is your message?")]
        public string Message { get; set; }
    }
}
