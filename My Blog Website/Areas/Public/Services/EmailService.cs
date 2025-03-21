using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using My_Blog_Website.Areas.Public.Interfaces;

namespace My_Blog_Website.Areas.Public.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendNewPostNotificationAsync(string toEmail, string postTitle, string postDescription, string postCategory, string postSlug)
        {
            var smtpSettings = _config.GetSection("SmtpSettings");

            using var client = new SmtpClient();

            try
            {
                // Explicit STARTTLS configuration for port 587
                await client.ConnectAsync(
                    smtpSettings["Host"],
                    int.Parse(smtpSettings["Port"]),
                    SecureSocketOptions.StartTls); // Explicit StartTls

                await client.AuthenticateAsync(
                    smtpSettings["Username"],
                    smtpSettings["Password"]);

                // Create message
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("radipu Blog", smtpSettings["Username"]));
                message.To.Add(new MailboxAddress("Subscriber", toEmail));
                message.Subject = $"New Post: {postTitle}";

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $@"
                        <!DOCTYPE html>
                        <html>
                        <head>
                            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                            <style>
                                @import url('https://fonts.googleapis.com/css2?family=Roboto:wght@300;400;500&display=swap');
                                body {{ margin: 0; padding: 0; font-family: 'Roboto', Arial, sans-serif; line-height: 1.6; }}
                                .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                                .header {{ background: linear-gradient(135deg, #6366f1, #4f46e5); padding: 30px; text-align: center; border-radius: 8px 8px 0 0; }}
                                .header h1 {{ color: #ffffff; margin: 0; font-size: 28px; }}
                                .content {{ padding: 30px; background: #ffffff; border-radius: 0 0 8px 8px; box-shadow: 0 2px 15px rgba(0,0,0,0.1); }}
                                .post-title {{ color: #1f2937; font-size: 22px; margin-bottom: 15px; }}
                                .cta-button {{ display: inline-block; background: #6366f1; color: #ffffff!important; padding: 12px 25px; text-decoration: none; border-radius: 5px; margin: 20px 0; }}
                                .footer {{ text-align: center; padding: 20px; color: #6b7280; font-size: 14px; }}
                                .social-links {{ margin: 20px 0; }}
                                .social-links a {{ margin: 0 10px; text-decoration: none; color: #6366f1!important; }}
                                @media (max-width: 600px) {{ .container {{ width: 100%!important; }} }}
                            </style>
                        </head>
                        <body>
                            <div class='container'>
                                <div class='header'>
                                    <h1>📬 New Post Alert!</h1>
                                </div>
            
                                <div class='content'>
                                    <h2 class='post-title'>{postTitle}</h2>
                
                                    <p>Hello valued reader,</p>
                
                                    <p>We're excited to share our latest article with you. Here's what you'll discover:</p>
                
                                    <!-- Add post summary/excerpt here -->
                                    <div style='background: #f8fafc; padding: 15px; border-left: 3px solid #6366f1; margin: 20px 0;'>
                                        <p style='margin: 0; color: #4b5563;'>{postDescription}</p>
                                    </div>
                
                                    <a href='{_config["radipu.com"]}/{postCategory}/{postSlug}' class='cta-button'>Read Full Article →</a>
                
                                    <div class='social-links'>
                                        Stay connected:
                                        <a href='https://fb.com/radipu13'>Facebook</a> • 
                                        <a href='https://twitter.com/radipu'>Twitter</a> • 
                                        <a href='#'>Instagram</a>
                                    </div>
                                </div>
            
                                <div class='footer'>
                                    <p>© {DateTime.Now.Year} {_config["BlogName"]}. All rights reserved.</p>
                                    <p style='font-size: 12px; color: #9ca3af;'>
                                        You received this email because you subscribed to our newsletter.<br>
                                        <a href='{_config["BlogUrl"]}/unsubscribe' style='color: #9ca3af;'>Unsubscribe</a> | 
                                        <a href='#' style='color: #9ca3af;'>View in Browser</a>
                                    </p>
                                </div>
                            </div>
                        </body>
                    </html>"
                };

                message.Body = bodyBuilder.ToMessageBody();

                await client.SendAsync(message);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}