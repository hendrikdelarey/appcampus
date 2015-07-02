using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.CodeDom.Compiler;

namespace AppCampus.Infrastructure.Models.Mapping
{
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public class DeviceLogTableMap : EntityTypeConfiguration<DeviceLogTable>
    {
        public DeviceLogTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(50);


            // Relationships
            this.HasOptional(t => t.LogFile)
                .WithMany(t => t.DeviceLogs)
                .HasForeignKey(d => d.LogFileId);

        }
    }
}
