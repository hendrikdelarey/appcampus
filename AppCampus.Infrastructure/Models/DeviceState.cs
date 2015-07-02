using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("DeviceState")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class DeviceStateTable : IDataEntity<Guid>
    {
 
        [Column("DeviceStateId")] 
		public System.Guid Id { get; set; }
 
        [Column("DeviceId")] 
		public System.Guid DeviceId { get; set; }
 
        [Column("State")] 
		public string State { get; set; }
 
        [Column("ChangedDate")] 
		public System.DateTime ChangedDate { get; set; }
        public virtual DeviceTable Device { get; set; }

    }
}
