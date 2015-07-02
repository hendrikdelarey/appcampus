using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("UserClaim", Schema="identity")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class UserClaimTable : IDataEntity<Guid>
    {
 
        [Column("Id")] 
		public System.Guid Id { get; set; }
 
        [Column("UserId")] 
		public System.Guid UserId { get; set; }
 
        [Column("ClaimType")] 
		public string ClaimType { get; set; }
 
        [Column("ClaimValue")] 
		public string ClaimValue { get; set; }
        public virtual UserTable User { get; set; }

    }
}
