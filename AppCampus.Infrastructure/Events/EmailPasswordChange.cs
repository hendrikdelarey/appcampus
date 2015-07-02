using AppCampus.Domain.Models.Events;
using AppCampus.Infrastructure.Modules.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Events;

namespace AppCampus.Infrastructure.Events
{
    public class EmailPasswordChange : IDomainEventHandler<PasswordChanged>
    {
        public void Handle(PasswordChanged args)
        {
            EmailClient client = new EmailClient();
            client.Send(new MailAddress(args.Username), "AppCampus Password Reset", String.Format("<p>Hi {0} {1},</p><p>Your AppCampus password has been reset.</p><p>Your new password is <b>{2}</b></p><p>Please sign in to change it.</p><p>Regards,</p><p>The AppCampus Team</p><p><a href=mailto:support@Drumble.co.za>support@Drumble.co.za</a></p>", args.FirstName, args.LastName, args.Password));
        }
    }
}
