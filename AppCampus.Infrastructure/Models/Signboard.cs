using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("Signboard")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class SignboardTable : IDataEntity<Guid>
    {
        public SignboardTable()
        {
            this.SignboardAnnouncements = new List<SignboardAnnouncementTable>();
            this.SignboardRequests = new List<SignboardRequestTable>();
            this.SignboardSlideshows = new List<SignboardSlideshowTable>();
        }
 
        [Column("SignboardId")] 
		public System.Guid Id { get; set; }
 
        [Column("CompanyId")] 
		public System.Guid CompanyId { get; set; }
 
        [Column("Name")] 
		public string Name { get; set; }
 
        [Column("CreatedDate")] 
		public System.DateTime CreatedDate { get; set; }
 
        [Column("DeviceId")] 
		public System.Guid DeviceId { get; set; }
 
        [Column("SoftwareVersion")] 
		public string SoftwareVersion { get; set; }
 
        [Column("FontFactor")] 
		public double FontFactor { get; set; }
 
        [Column("IsShowingScreensaver")] 
		public bool IsShowingScreensaver { get; set; }
        public virtual CompanyTable Company { get; set; }

        public virtual DeviceTable Device { get; set; }


        public virtual ICollection<SignboardAnnouncementTable> SignboardAnnouncements { get; set; }


        public virtual ICollection<SignboardRequestTable> SignboardRequests { get; set; }


        public virtual ICollection<SignboardSlideshowTable> SignboardSlideshows { get; set; }

    }
}
