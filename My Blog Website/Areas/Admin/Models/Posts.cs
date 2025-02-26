using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace My_Blog_Website.Areas.Admin.Models
{
   public class Posts
    {
        [Key]
        public int PostId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string PostContent { get; set; }
        public byte[] FeatureImage { get; set; }
        [Required]
        public string Categories { get; set; }
        public string Tags { get; set; }
        public string Author { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}
