using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.CodeDom.Compiler;

namespace AppCampus.Infrastructure.Models.Mapping
{
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public class SignboardAnnouncementTableMap : EntityTypeConfiguration<SignboardAnnouncementTable>
    {
        public SignboardAnnouncementTableMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

            // Relationships
            this.HasRequired(t => t.Announcement)
                .WithMany(t => t.SignboardAnnouncements)
                .HasForeignKey(d => d.AnnouncementId);
            this.HasRequired(t => t.Signboard)
                .WithMany(t => t.SignboardAnnouncements)
                .HasForeignKey(d => d.SignboardId);

        }
    }
}
