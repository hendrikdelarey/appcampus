using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("SignboardRequest")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class SignboardRequestTable : IDataEntity<Guid>
    {
 
        [Column("SignboardRequestId")] 
		public System.Guid Id { get; set; }
 
        [Column("SignboardId")] 
		public System.Guid SignboardId { get; set; }
 
        [Column("RequestType")] 
		public string RequestType { get; set; }
 
        [Column("IsSent")] 
		public bool IsSent { get; set; }
 
        [Column("IsProcessed")] 
		public bool IsProcessed { get; set; }
 
        [Column("IsCancelled")] 
		public bool IsCancelled { get; set; }
 
        [Column("Value")] 
		public string Value { get; set; }
 
        [Column("CreatedDate")] 
		public System.DateTime CreatedDate { get; set; }
        public virtual SignboardTable Signboard { get; set; }

    }
}
