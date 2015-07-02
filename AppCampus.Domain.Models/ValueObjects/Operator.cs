using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.ValueObjects
{
    public class Operator : IValueObject<Operator>
    {
        public string Name { get; private set; }

        public string DisplayName { get; private set; }

        public IEnumerable<string> Modes { get; private set; }

        public string Category { get; private set; }

        private Operator(string name, string displayName, IEnumerable<string> modes, string category) 
        {
            Name = name;
            DisplayName = displayName;
            Modes = modes;
            Category = category;
        }

        public static Operator From(string name, string displayName, IEnumerable<string> modes, string category) 
        {
            return new Operator(name, displayName, modes, category);
        }

        public bool Equals(Operator other)
        {
            if (other == null)
            {
                return false;
            }

            return other.Name == Name &&
                   other.DisplayName == DisplayName &&
                   other.Category == Category &&
                   other.Modes.SequenceEqual(Modes);
        }
    }
}
