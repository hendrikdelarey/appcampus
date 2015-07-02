using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.CodeDom.Compiler;

namespace AppCampus.Infrastructure.Models.Mapping
{
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public class UserRoleTableMap : EntityTypeConfiguration<UserRoleTable>
    {
        public UserRoleTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

            // Relationships
            this.HasRequired(t => t.Role)
                .WithMany(t => t.UserRoles)
                .HasForeignKey(d => d.RoleId);
            this.HasRequired(t => t.User)
                .WithMany(t => t.UserRoles)
                .HasForeignKey(d => d.UserId);

        }
    }
}
