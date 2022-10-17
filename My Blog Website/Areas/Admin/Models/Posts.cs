using System.ComponentModel.DataAnnotations;

namespace My_Blog_Website.Areas.Admin.Models
{
   public class Posts
    {
        [Key]
        public int PostId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public string FeatureImage { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Tags { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
    }
}
