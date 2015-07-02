using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.CodeDom.Compiler;

namespace AppCampus.Infrastructure.Models.Mapping
{
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public class AnnouncementTableMap : EntityTypeConfiguration<AnnouncementTable>
    {
        public AnnouncementTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Message)
                .IsRequired()
                .HasMaxLength(256);

            this.Property(t => t.Severity)
                .IsRequired()
                .HasMaxLength(16);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(64);


            // Relationships
            this.HasRequired(t => t.Company)
                .WithMany(t => t.Announcements)
                .HasForeignKey(d => d.CompanyId);

        }
    }
}
