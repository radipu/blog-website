using System.ComponentModel.DataAnnotations;

namespace My_Blog_Website.Models
{
    public class Contacts
    {
        [Key]
        public int MsgId { get; set; }
        
        [Required(ErrorMessage = "How should I address you?")]
        public string Name { get; set; }

        [Required(ErrorMessage = "I need your email for further communication.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "What is the subject?")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "What is your message?")]
        [MaxLength(1000, ErrorMessage = "Your message is too long.")]
        [MinLength(250, ErrorMessage = "Your message is too short.")]
        public string Message { get; set; }
    }
}
