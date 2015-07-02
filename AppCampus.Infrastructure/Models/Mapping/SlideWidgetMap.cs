using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.CodeDom.Compiler;

namespace AppCampus.Infrastructure.Models.Mapping
{
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public class SlideWidgetTableMap : EntityTypeConfiguration<SlideWidgetTable>
    {
        public SlideWidgetTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

            // Relationships
            this.HasRequired(t => t.Slide)
                .WithMany(t => t.SlideWidgets)
                .HasForeignKey(d => d.SlideId);
            this.HasRequired(t => t.WidgetDefinition)
                .WithMany(t => t.SlideWidgets)
                .HasForeignKey(d => d.WidgetDefinitionId);

        }
    }
}
