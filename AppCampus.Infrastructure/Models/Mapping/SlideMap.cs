using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.CodeDom.Compiler;

namespace AppCampus.Infrastructure.Models.Mapping
{
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public class SlideTableMap : EntityTypeConfiguration<SlideTable>
    {
        public SlideTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Transition)
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.BackgroundColour)
                .HasMaxLength(50);


            // Relationships
            this.HasRequired(t => t.Slideshow)
                .WithMany(t => t.Slides)
                .HasForeignKey(d => d.SlideshowId);

        }
    }
}
