using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.CodeDom.Compiler;

namespace AppCampus.Infrastructure.Models.Mapping
{
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public class DeviceTableMap : EntityTypeConfiguration<DeviceTable>
    {
        public DeviceTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.MacAddress)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Comment)
                .HasMaxLength(256);


            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Devices)
                .HasForeignKey(d => d.CompanyId);

        }
    }
}
