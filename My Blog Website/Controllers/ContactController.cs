using Microsoft.AspNetCore.Mvc;
using My_Blog_Website.Data;
using My_Blog_Website.Models;
using System.Net;
using System.Net.Mail;

namespace My_Blog_Website.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Contacts contact)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("rad.edu.du@gmail.com");
                mail.To.Add("radipu.dev@gmail.com");
                //mail.CC.Add("dipuxoo@gmail.com");
                //mail.Bcc.Add("alonedipu24@gmail.com");
                mail.Subject = contact.Subject;
                mail.IsBodyHtml = true;
                string content = "Name: " + contact.Name + "<br/>" + "Email: " + contact.Email + "<br/>" + "Message: " + contact.Message;
                mail.Body = content;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");

                NetworkCredential networkCredential = new NetworkCredential("rad.edu.du@gmail.com", "coarfkxrlfwxswrr");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = networkCredential;
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.Send(mail);
                ViewBag.Message = "Message has been sent successfully";
                ModelState.Clear();
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message.ToString();
            }
            return View();
        }
    }
}
