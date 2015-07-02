using Drumble.DomainDrivenArchitecture.Domain.Events;

namespace AppCampus.Domain.Models.Events
{
    public class PasswordChanged : IDomainEvent
    {
        public string Username { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Password { get; private set; }

        public PasswordChanged(string username, string firstName, string lastName, string password)
        {
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
        }
    }
}