using AppCampus.Infrastructure.Components;
using Microsoft.Azure;
using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppCampus.Infrastructure.Modules.Email
{
    public class EmailClient
    {
        public EmailClient()
        {

        }

        public void Send(MailAddress toAddress, string subject, string message)
        {
            var fromAddress = CloudConfigurationManager.GetSetting("Email.NoReply");

            var email = new MailMessage();

            email.From = new MailAddress(fromAddress);
            email.Sender = new MailAddress(fromAddress);
            
            email.ReplyToList.Add(new MailAddress(fromAddress));
            email.To.Add(toAddress);
            
            email.Subject = subject;
            email.Body = message;
            email.IsBodyHtml = true;

            Task.Factory.StartNew(() =>
            {
                using(var smtpClient = new SmtpClient())
                {
                    smtpClient.Send(email);
                }
            })
            .ContinueWith(t =>
            {
                LoggingComponent.Instance.LogWarning(MethodBase.GetCurrentMethod(), "SMTP Email Send failed: " + t.Exception);
            }, TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}
