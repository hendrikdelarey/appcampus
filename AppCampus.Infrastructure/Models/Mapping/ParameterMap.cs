using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.CodeDom.Compiler;

namespace AppCampus.Infrastructure.Models.Mapping
{
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public class ParameterTableMap : EntityTypeConfiguration<ParameterTable>
    {
        public ParameterTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Value)
                .IsRequired()
                .HasMaxLength(200);


            // Relationships
            this.HasRequired(t => t.ParameterDefinition)
                .WithMany(t => t.Parameters)
                .HasForeignKey(d => d.ParameterDefinitionId);
            this.HasOptional(t => t.SlideWidget)
                .WithMany(t => t.Parameters)
                .HasForeignKey(d => d.SlideWidgetId);

        }
    }
}
