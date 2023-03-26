using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;

namespace DoanNhom10.Models
{
    public class EmailServiceNew : IIdentityMessageService
    {
        public static async Task SendEmail(IdentityMessage message, string attachedFile =
null)
        {
            // Plug in your email service here to send an email.
            await configSendGridasync(message, attachedFile);
        }
        public async Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            await configSendGridasync(message);
        }
        private static async Task configSendGridasync(IdentityMessage message, string
       attachedFile = null)
        {
         
            string fromEmail = "xyz@gmail.com";
            string fromPassword = "aaa";
            var fromAddress = new MailAddress("CRM@gmail.com", "Ban hang BIS - UEH");
            var toAddress = new MailAddress(message.Destination,
           message.Destination);
            string subject = message.Subject;
            string body = message.Body;
            string html = message.Body;
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail, fromPassword)
            };
            var messageSend = new MailMessage(fromAddress, toAddress);
            messageSend.Subject = subject;

      

            messageSend.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null,
            MediaTypeNames.Text.Html));
            if (!string.IsNullOrEmpty(attachedFile))
            {
                Attachment data = new Attachment(
                attachedFile,
               MediaTypeNames.Application.Octet);
                messageSend.Attachments.Add(data);
            }
            smtp.Send(messageSend);
        }
       
    }

}