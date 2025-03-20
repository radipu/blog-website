namespace My_Blog_Website.Areas.Public.Models
{
    public class Reaction
    {
        public int ReactionId { get; set; }
        public int CommentId { get; set; }
        public string ReactionType { get; set; }
        public int ReactionCount { get; set; }

        // Navigation property
        public Comment Comment { get; set; }
    }
}
