using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("UserRole", Schema="identity")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class UserRoleTable : IDataEntity<Guid>
    {
 
        [Column("UserRoleId")] 
		public System.Guid Id { get; set; }
 
        [Column("UserId")] 
		public System.Guid UserId { get; set; }
 
        [Column("RoleId")] 
		public System.Guid RoleId { get; set; }
        public virtual RoleTable Role { get; set; }

        public virtual UserTable User { get; set; }

    }
}
