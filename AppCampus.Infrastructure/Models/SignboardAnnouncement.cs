using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("SignboardAnnouncement")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class SignboardAnnouncementTable : IDataEntity<Guid>
    {
 
        [Column("SignboardAnnouncementId")] 
		public System.Guid Id { get; set; }
 
        [Column("AnnouncementId")] 
		public System.Guid AnnouncementId { get; set; }
 
        [Column("SignboardId")] 
		public System.Guid SignboardId { get; set; }
        public virtual AnnouncementTable Announcement { get; set; }

        public virtual SignboardTable Signboard { get; set; }

    }
}
