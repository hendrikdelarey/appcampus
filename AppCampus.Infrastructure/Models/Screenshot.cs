using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("Screenshot")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class ScreenshotTable : IDataEntity<Guid>
    {
 
        [Column("SignboardScreenshotId")] 
		public System.Guid Id { get; set; }
 
        [Column("DeviceId")] 
		public System.Guid DeviceId { get; set; }
 
        [Column("Base64ImageString")] 
		public string Base64ImageString { get; set; }
 
        [Column("CreatedDate")] 
		public System.DateTime CreatedDate { get; set; }
    }
}
