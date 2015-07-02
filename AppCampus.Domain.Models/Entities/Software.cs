using System;
using System.Collections.Generic;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.Entities
{
    public class Software : DomainEntity<Guid>, IAggregateRoot
    {
        public Version Version { get; private set; }

        public string Comment { get; private set; }

        private Software(Guid id, Version version, string comment)
            : base(id)
        {
            if (String.IsNullOrWhiteSpace(comment)) 
            {
                comment = String.Empty;
            }

            Version = version;
            Comment = comment;
        }

        public Software(Version version, string comment)
            : this(CombIdentityFactory.GenerateIdentity(), version, comment)
        {
        }

        public static Software Hydrate(Guid id, Version version, string comment)
        {
            return new Software(id, version, comment);
        }
    }
}