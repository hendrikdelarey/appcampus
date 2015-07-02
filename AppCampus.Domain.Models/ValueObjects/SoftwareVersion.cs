using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.ValueObjects
{
    public class SoftwareVersion : IValueObject<SoftwareVersion>
    {
        public string VersionNumber { get; private set; }

        private SoftwareVersion(string version)
        {
            VersionNumber = version;
        }

        public bool Equals(SoftwareVersion other)
        {
            if(other == null)
            {
                return false;
            }

            return other.VersionNumber == VersionNumber;
        }

        public static SoftwareVersion From(string version)
        {
            return new SoftwareVersion(version);
        }
    }
}
