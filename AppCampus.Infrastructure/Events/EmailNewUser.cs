using AppCampus.Domain.Models.Events;
using AppCampus.Infrastructure.Modules.Email;
using System;
using System.Net.Mail;
using Drumble.DomainDrivenArchitecture.Domain.Events;

namespace AppCampus.Infrastructure.Events
{
    public class EmailNewUser : IDomainEventHandler<UserCreated>
    {
        public void Handle(UserCreated args)
        {
            EmailClient client = new EmailClient();
            client.Send(new MailAddress(args.Username), "AppCampus Account Created", String.Format("<p>Hi {0} {1},</p><p>Your AppCampus account has been created.</p><p>Your password is <b>{2}</b></p><p>Please sign in to change it.</p><p>Regards,</p><p>The AppCampus Team</p><p><a href=mailto:support@Drumble.co.za>support@Drumble.co.za</a></p>", args.FirstName, args.LastName, args.Password));
        }
    }
}