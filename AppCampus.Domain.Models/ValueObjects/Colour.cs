using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AppCampus.Domain.Models.ValueObjects
{
    public class Colour : IEquatable<Colour>
    {
        public string ColourHex { get; private set; }

        public Colour(string colourHex)
        {
            if (IsColourFormat(colourHex))
            {
                ColourHex = colourHex;
            }
            else
            {
                throw new ArgumentOutOfRangeException("colourHex", "Colour must be a valid hexcode");
            }
        }

        public static bool IsColourFormat(string colour)
        {
            var match = Regex.Match(colour, "#[0-9a-fA-F]{6}$");

            return match.Success;
        }

        public bool Equals(Colour other)
        {
            return (other != null && other.ColourHex.Equals(ColourHex, StringComparison.InvariantCultureIgnoreCase));
        }
    }

}
