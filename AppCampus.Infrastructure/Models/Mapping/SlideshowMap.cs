using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.CodeDom.Compiler;

namespace AppCampus.Infrastructure.Models.Mapping
{
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public class SlideshowTableMap : EntityTypeConfiguration<SlideshowTable>
    {
        public SlideshowTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);


            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Slideshows)
                .HasForeignKey(d => d.CompanyId);

        }
    }
}