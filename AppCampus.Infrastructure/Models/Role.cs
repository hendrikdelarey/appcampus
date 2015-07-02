using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("Role", Schema="identity")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class RoleTable : IDataEntity<Guid>
    {
        public RoleTable()
        {
            this.UserRoles = new List<UserRoleTable>();
        }
 
        [Column("Id")] 
		public System.Guid Id { get; set; }
 
        [Column("Name")] 
		public string Name { get; set; }

        public virtual ICollection<UserRoleTable> UserRoles { get; set; }

    }
}
