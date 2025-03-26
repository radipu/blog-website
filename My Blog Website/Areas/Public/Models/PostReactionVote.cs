using My_Blog_Website.Areas.Admin.Models;

namespace My_Blog_Website.Areas.Public.Models
{
    public class PostReactionVote
    {
        public int PostReactionVoteId { get; set; }
        public int PostId { get; set; }
        public string UserIdentifier { get; set; }
        public string ReactionType { get; set; }
        public DateTime VoteDate { get; set; }

        public Posts Post { get; set; }
    }
}