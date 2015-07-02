using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.CodeDom.Compiler;

namespace AppCampus.Infrastructure.Models.Mapping
{
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public class SoftwareTableMap : EntityTypeConfiguration<SoftwareTable>
    {
        public SoftwareTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .HasMaxLength(20);


            // Relationships
            this.HasOptional(t => t.SoftwareFile)
                .WithMany(t => t.Softwares)
                .HasForeignKey(d => d.SoftwareFileId);

        }
    }
}
