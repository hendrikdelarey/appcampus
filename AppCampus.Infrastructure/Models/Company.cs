using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("Company")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class CompanyTable : IDataEntity<Guid>
    {
        public CompanyTable()
        {
            this.Announcements = new List<AnnouncementTable>();
            this.Devices = new List<DeviceTable>();
            this.Signboards = new List<SignboardTable>();
            this.Slideshows = new List<SlideshowTable>();
            this.Users = new List<UserTable>();
        }
 
        [Column("CompanyId")] 
		public System.Guid Id { get; set; }
 
        [Column("Name")] 
		public string Name { get; set; }

        public virtual ICollection<AnnouncementTable> Announcements { get; set; }


        public virtual ICollection<DeviceTable> Devices { get; set; }


        public virtual ICollection<SignboardTable> Signboards { get; set; }


        public virtual ICollection<SlideshowTable> Slideshows { get; set; }


        public virtual ICollection<UserTable> Users { get; set; }

    }
}
