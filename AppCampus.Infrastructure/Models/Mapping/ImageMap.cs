using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.CodeDom.Compiler;

namespace AppCampus.Infrastructure.Models.Mapping
{
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public class ImageTableMap : EntityTypeConfiguration<ImageTable>
    {
        public ImageTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Base64Image)
                .IsRequired();

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(64);

        }
    }
}
