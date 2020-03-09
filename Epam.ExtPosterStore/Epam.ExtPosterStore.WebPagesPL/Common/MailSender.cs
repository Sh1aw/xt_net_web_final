using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;

namespace Epam.ExtPosterStore.WebPagesPL.Common
{
    public class MailSender
    {
        public void send()
        {
            WebMail.EnableSsl = true;
            WebMail.SmtpServer = "smtp.gmail.com";
            WebMail.SmtpPort = 587;
            WebMail.UserName = "eLSUPms";
            WebMail.Password = "!hjf@@334A";
            WebMail.From = "eLSUPms@gmail.com";

            // Send email
            WebMail.Send(to: "eSuba.inc@yandex.ru",
                subject: "Help request from",
                body: "Hi"
            );
        }
        
    }
}