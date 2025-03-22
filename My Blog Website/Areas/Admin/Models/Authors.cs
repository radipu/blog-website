using System.ComponentModel.DataAnnotations;

namespace My_Blog_Website.Areas.Admin.Models
{
    public class Authors
    {
        [Key]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "First Name is required!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        public string? AboutAuth { get; set; }

        public byte[] AuthorImage { get; set; }
        public string ImageContentType { get; set; }
    }
}
