using My_Blog_Website.Areas.Public.Models;
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

        public string? PostDescription { get; set; }

        public string FeatureImageUrl { get; set; } // Store URL or Base64

        [Required]
        public string Categories { get; set; }

        public string? Slug { get; set; } // Generated from Title (e.g., "how-to-write-articles")

        public string Tags { get; set; }

        public string PostStatus { get; set; }

        public DateTime? PublishedDate { get; set; }

        public ICollection<Comment>? Comments { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? LastModifiedDate { get; set; }

        public int? ViewCount { get; set; } = 0;

        public ICollection<PostReactionVote> PostReactionVotes { get; set; } = new List<PostReactionVote>();
    }
}