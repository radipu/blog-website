using System.ComponentModel.DataAnnotations;

namespace My_Blog_Website.Models
{
    public class FAQs
    {
        [Key]
        public int FAQid { get; set; }
        public string FAQuestion { get; set; }
        public string? FAQanswer { get; set; }
    }
}