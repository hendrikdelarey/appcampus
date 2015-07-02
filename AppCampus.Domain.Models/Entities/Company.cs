using System;
using System.Collections.Generic;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.Entities
{
    public class Company : DomainEntity<Guid>, IAggregateRoot
    {
        private string name;

        public string Name 
        {
            get
            {
                return name;
            }
            private set
            { 
                if(String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentOutOfRangeException("value", "Company name cannot be empty or null");
                }

                name = value;
            }
        }

        public Company(string name)
            : this(CombIdentityFactory.GenerateIdentity(), name)
        {
        }

        private Company(Guid id, string name)
            : base(id)
        {
            Name = name;
        }

        public static Company Hydrate(Guid id, string name)
        {
            return new Company(id, name);
        }
    }
}