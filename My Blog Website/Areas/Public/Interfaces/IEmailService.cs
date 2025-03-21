using MimeKit;
using MailKit.Net.Smtp; 
using MailKit.Security;

namespace My_Blog_Website.Areas.Public.Interfaces
{
    public interface IEmailService
    {
        Task SendNewPostNotificationAsync(string toEmail, string postTitle, string postDescription, string postCategory, string postSlug);
    }
}