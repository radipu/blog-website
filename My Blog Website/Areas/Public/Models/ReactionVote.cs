namespace My_Blog_Website.Areas.Public.Models
{
    public class ReactionVote
    {
        public int ReactionVoteId { get; set; }
        public int CommentId { get; set; }
        public string UserIdentifier { get; set; }
        public string ReactionType { get; set; }
        public DateTime VoteDate { get; set; }

        public Comment Comment { get; set; }
    }
}