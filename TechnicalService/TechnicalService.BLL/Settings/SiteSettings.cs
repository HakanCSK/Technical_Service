﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TechnicalService.Entity.ViewsModel;

namespace TechnicalService.BLL.Settings
{

    public static class SiteSettings
    {

        public static string SiteMail = "kamilfidil@gmail.com";
        public static string SiteMailPassword = "123456kf";
        public static string SiteMailSmtpHost = "smtp.gmail.com";
        public static int SiteMailSmtpPort = 587;
        public static bool SiteMailEnableSsl = true;

        public async static Task SendMail(MailModel model)
        {
            using (var smtp = new SmtpClient())
            {
                var message = new MailMessage();
                message.To.Add(new MailAddress(model.To));
                message.From = new MailAddress(SiteMail);
                message.Subject = model.Subject;
                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.UTF8;
                message.Body = model.Message;
                if (!string.IsNullOrEmpty(model.Cc))
                    message.CC.Add(new MailAddress(model.Cc));
                if (!string.IsNullOrEmpty(model.Bcc))
                    message.Bcc.Add(new MailAddress(model.Bcc));

                var credential = new NetworkCredential()
                {
                    UserName = SiteMail,
                    Password = SiteMailPassword
                };
                smtp.Credentials = credential;
                smtp.Host = SiteMailSmtpHost;
                smtp.Port = SiteMailSmtpPort;
                smtp.EnableSsl = SiteMailEnableSsl;
                await smtp.SendMailAsync(message);
            }

        }

    }
}
