using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.ValueObjects
{
    public class Stop : IValueObject<Stop>
    {
        public string Name { get; private set; }

        public string Code { get; private set; }

        public string Operator { get; private set; }

        public string Mode { get; private set; }

        public static Stop From(string name, string code, string op, string mode)
        {
            return new Stop()
            {
                Name = name,
                Code = code,
                Operator = op,
                Mode = mode
            };
        }

        public bool Equals(Stop other)
        {
            if (other == null)
            {
                return false;
            }

            return other.Code == Code; 
        }
    }
}
