namespace My_Blog_Website.Areas.Public.Models
{
    public class Subscriber
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public bool IsActive { get; set; }
    }
}
