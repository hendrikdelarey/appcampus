using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.CodeDom.Compiler;

namespace AppCampus.Infrastructure.Models.Mapping
{
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public class ParameterDefinitionTableMap : EntityTypeConfiguration<ParameterDefinitionTable>
    {
        public ParameterDefinitionTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.DefaultValue)
                .HasMaxLength(200);


            // Relationships
            this.HasRequired(t => t.WidgetDefinition)
                .WithMany(t => t.ParameterDefinitions)
                .HasForeignKey(d => d.WidgetDefinitionId);

        }
    }
}
