using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.CodeDom.Compiler;

namespace AppCampus.Infrastructure.Models.Mapping
{
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public class DeviceStateTableMap : EntityTypeConfiguration<DeviceStateTable>
    {
        public DeviceStateTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.State)
                .IsRequired()
                .HasMaxLength(20);


            // Relationships
            this.HasRequired(t => t.Device)
                .WithMany(t => t.DeviceStates)
                .HasForeignKey(d => d.DeviceId);

        }
    }
}
