using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.Entities
{
    public class SoftwareComment  : DomainEntity<Guid>    
    {
        public string Comment { get; private set; }

        // Hydrate
        private SoftwareComment(Guid softwareCommentId, string comment)
            : base(softwareCommentId)
        {
            Comment = comment;
        }

        public SoftwareComment(string comment)
            : base(CombIdentityFactory.GenerateIdentity())
        {
            Comment = comment;
        }

         public static SoftwareComment Hydrate(Guid softwareCommentId, string comment) 
        {
            return new SoftwareComment(softwareCommentId, comment);
        }
    }
}
