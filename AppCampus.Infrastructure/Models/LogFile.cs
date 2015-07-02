using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("LogFile")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class LogFileTable : IDataEntity<Guid>
    {
        public LogFileTable()
        {
            this.DeviceLogs = new List<DeviceLogTable>();
        }
 
        [Column("LogFileId")] 
		public System.Guid Id { get; set; }
 
        [Column("Binary")] 
		public byte[] Binary { get; set; }

        public virtual ICollection<DeviceLogTable> DeviceLogs { get; set; }

    }
}
