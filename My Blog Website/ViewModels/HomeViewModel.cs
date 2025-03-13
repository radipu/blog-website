using My_Blog_Website.Areas.Admin.Models;

namespace My_Blog_Website.ViewModels
{
    public class HomeViewModel
    {
        public List<Posts> LatestPost { get; set; }

        public List<Posts> Thoughts { get; set; }
        public List<Posts> Technology { get; set; }
        public List<Posts> Ideas { get; set; }
        public List<Posts> HowTo { get; set; }
        public List<Posts> Tour { get; set; }
        public List<Posts> Developer { get; set; }
        public List<Posts> BookReview { get; set; }
    }
}