using My_Blog_Website.Areas.Admin.Models;

namespace My_Blog_Website.Areas.Public.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int? ParentCommentId { get; set; }
        public string CommenterName { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }

        // Navigation properties
        public Posts Post { get; set; }
        public ICollection<Reaction> Reactions { get; set; }
        public Comment ParentComment { get; set; } // Link to the parent comment
        public ICollection<Comment> Replies { get; set; } // Collection of replies
    }
}
