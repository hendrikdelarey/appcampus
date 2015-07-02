using Microsoft.AspNet.Identity;
using System;
using System.Security.Claims;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.Identity
{
    public class ApplicationUser : IUser<Guid>
    {
        public Guid Id { get; private set; }

        public Guid CompanyId { get; private set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PasswordHash { get; private set; }

        public ApplicationUser(Guid companyId, string userName, string firstName, string lastName)
        {
            Id = CombIdentityFactory.GenerateIdentity();
            CompanyId = companyId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
        }

        private ApplicationUser(Guid id, Guid companyId, string userName, string firstName, string lastName)
        {
            Id = id;
            CompanyId = companyId;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
        }

        public void SetPasswordHash(string hash)
        {
            PasswordHash = hash;
        }

        public static ApplicationUser Hydrate(Guid id, Guid companyId, string userName, string firstName, string lastName, string passwordHash)
        {
            var user = new ApplicationUser(id, companyId, userName, firstName, lastName);
            user.SetPasswordHash(passwordHash);
            return user;
        }
    }
}