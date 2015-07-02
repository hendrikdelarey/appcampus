using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.CodeDom.Compiler;

namespace AppCampus.Infrastructure.Models.Mapping
{
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public class SignboardRequestTableMap : EntityTypeConfiguration<SignboardRequestTable>
    {
        public SignboardRequestTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.RequestType)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Value)
                .HasMaxLength(64);


            // Relationships
            this.HasRequired(t => t.Signboard)
                .WithMany(t => t.SignboardRequests)
                .HasForeignKey(d => d.SignboardId);

        }
    }
}
