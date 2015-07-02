using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.CodeDom.Compiler;

namespace AppCampus.Infrastructure.Models.Mapping
{
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public class SoftwareFileTableMap : EntityTypeConfiguration<SoftwareFileTable>
    {
        public SoftwareFileTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Binary)
                .IsRequired();

        }
    }
}
