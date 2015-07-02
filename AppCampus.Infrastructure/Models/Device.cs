using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("Device")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class DeviceTable : IDataEntity<Guid>
    {
        public DeviceTable()
        {
            this.DeviceStates = new List<DeviceStateTable>();
            this.Signboards = new List<SignboardTable>();
        }
 
        [Column("DeviceId")] 
		public System.Guid Id { get; set; }
 
        [Column("CompanyId")] 
		public System.Guid CompanyId { get; set; }
 
        [Column("MacAddress")] 
		public string MacAddress { get; set; }
 
        [Column("CreatedDate")] 
		public System.DateTime CreatedDate { get; set; }
 
        [Column("Comment")] 
		public string Comment { get; set; }
        public virtual CompanyTable Company { get; set; }


        public virtual ICollection<DeviceStateTable> DeviceStates { get; set; }


        public virtual ICollection<SignboardTable> Signboards { get; set; }

    }
}
