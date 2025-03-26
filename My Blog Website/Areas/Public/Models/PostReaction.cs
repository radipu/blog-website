using My_Blog_Website.Areas.Admin.Models;

namespace My_Blog_Website.Areas.Public.Models
{
    public class PostReaction
    {
        public int PostReactionId { get; set; }
        public int PostId { get; set; }
        public string ReactionType { get; set; }
        public int ReactionCount { get; set; }
        public Posts Post { get; set; }
    }
}