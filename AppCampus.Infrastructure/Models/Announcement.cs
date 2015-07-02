using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("Announcement")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class AnnouncementTable : IDataEntity<Guid>
    {
        public AnnouncementTable()
        {
            this.SignboardAnnouncements = new List<SignboardAnnouncementTable>();
        }
 
        [Column("AnnouncementId")] 
		public System.Guid Id { get; set; }
 
        [Column("Message")] 
		public string Message { get; set; }
 
        [Column("StartDate")] 
		public System.DateTime StartDate { get; set; }
 
        [Column("EndDate")] 
		public System.DateTime EndDate { get; set; }
 
        [Column("Severity")] 
		public string Severity { get; set; }
 
        [Column("CompanyId")] 
		public System.Guid CompanyId { get; set; }
 
        [Column("IsActive")] 
		public bool IsActive { get; set; }
 
        [Column("Name")] 
		public string Name { get; set; }
 
        [Column("IsDeleted")] 
		public bool IsDeleted { get; set; }
        public virtual CompanyTable Company { get; set; }


        public virtual ICollection<SignboardAnnouncementTable> SignboardAnnouncements { get; set; }

    }
}
