using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.CodeDom.Compiler;

namespace AppCampus.Infrastructure.Models.Mapping
{
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public class SignboardTableMap : EntityTypeConfiguration<SignboardTable>
    {
        public SignboardTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.SoftwareVersion)
                .IsRequired()
                .HasMaxLength(10);


            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Signboards)
                .HasForeignKey(d => d.CompanyId);
            this.HasRequired(t => t.Device)
                .WithMany(t => t.Signboards)
                .HasForeignKey(d => d.DeviceId);

        }
    }
}
