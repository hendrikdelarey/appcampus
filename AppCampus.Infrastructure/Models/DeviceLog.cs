using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("DeviceLog")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class DeviceLogTable : IDataEntity<Guid>
    {
 
        [Column("DeviceLogId")] 
		public System.Guid Id { get; set; }
 
        [Column("LogFileId")] 
		public Nullable<System.Guid> LogFileId { get; set; }
 
        [Column("FileName")] 
		public string FileName { get; set; }
        public virtual LogFileTable LogFile { get; set; }

    }
}
