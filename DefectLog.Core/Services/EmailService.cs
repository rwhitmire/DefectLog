using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace DefectLog.Core.Services
{
    public interface IEmailService
    {
        void SendPasswordReset(string emailAddress, Guid? resetPasswordKey, string baseUrl);
    }

    public class EmailService : IEmailService
    {
        public void SendPasswordReset(string emailAddress, Guid? resetPasswordKey, string baseUrl)
        {
            var message = new MailMessage {From = new MailAddress("noreply@uerie.github.io")};
            message.To.Add(new MailAddress(emailAddress));
            message.Subject = "Defect Log - Reset Password";

            var link = baseUrl + "/Account/ResetPassword/" + resetPasswordKey;
            var anchor = new TagBuilder("a");
            
            anchor.MergeAttribute("href", link);
            anchor.SetInnerText("Click here");

            message.Body = anchor + " to reset your password.";
            message.IsBodyHtml = true;

            var client = new SmtpClient();

            var host = ConfigurationManager.AppSettings["SmtpHost"];

            if (host != null)
            {
                var userName = ConfigurationManager.AppSettings["SmtpUserName"];
                var password = ConfigurationManager.AppSettings["SmtpPassword"];
                var port = Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]);

                client.Host = host;
                client.Port = port;
                client.Credentials = new NetworkCredential(userName, password);
            }
            else
            {
                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.PickupDirectoryLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "Emails");
            }

            client.Send(message);
        }
    }
}