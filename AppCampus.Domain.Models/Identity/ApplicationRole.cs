using Microsoft.AspNet.Identity;
using System;

namespace AppCampus.Domain.Models.Identity
{
    public class ApplicationRole : IRole<Guid>
    {
        public Guid Id { get; private set; }

        public string Name { get; set; }

        public ApplicationRole(Guid id, string roleName)
        {
            Id = id;
            Name = roleName;
        }
    }
}