using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("Software")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class SoftwareTable : IDataEntity<Guid>
    {
 
        [Column("SoftwareId")] 
		public System.Guid Id { get; set; }
 
        [Column("Version")] 
		public string Version { get; set; }
 
        [Column("SoftwareFileId")] 
		public Nullable<System.Guid> SoftwareFileId { get; set; }
 
        [Column("CreatedDate")] 
		public System.DateTime CreatedDate { get; set; }
 
        [Column("Comment")] 
		public string Comment { get; set; }
        public virtual SoftwareFileTable SoftwareFile { get; set; }

    }
}
