using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.CodeDom.Compiler;

namespace AppCampus.Infrastructure.Models.Mapping
{
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public class SignboardSlideshowTableMap : EntityTypeConfiguration<SignboardSlideshowTable>
    {
        public SignboardSlideshowTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

            // Relationships
            this.HasRequired(t => t.Signboard)
                .WithMany(t => t.SignboardSlideshows)
                .HasForeignKey(d => d.SignboardId);
            this.HasRequired(t => t.Slideshow)
                .WithMany(t => t.SignboardSlideshows)
                .HasForeignKey(d => d.SlideshowId);

        }
    }
}
