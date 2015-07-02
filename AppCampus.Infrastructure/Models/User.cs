using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("User", Schema="identity")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class UserTable : IDataEntity<Guid>
    {
        public UserTable()
        {
            this.UserClaims = new List<UserClaimTable>();
            this.UserRoles = new List<UserRoleTable>();
        }
 
        [Column("Id")] 
		public System.Guid Id { get; set; }
 
        [Column("CompanyId")] 
		public System.Guid CompanyId { get; set; }
 
        [Column("PasswordHash")] 
		public string PasswordHash { get; set; }
 
        [Column("UserName")] 
		public string UserName { get; set; }
 
        [Column("FirstName")] 
		public string FirstName { get; set; }
 
        [Column("LastName")] 
		public string LastName { get; set; }
        public virtual CompanyTable Company { get; set; }


        public virtual ICollection<UserClaimTable> UserClaims { get; set; }


        public virtual ICollection<UserRoleTable> UserRoles { get; set; }

    }
}
