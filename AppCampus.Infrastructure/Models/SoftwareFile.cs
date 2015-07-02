using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("SoftwareFile")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class SoftwareFileTable : IDataEntity<Guid>
    {
        public SoftwareFileTable()
        {
            this.Softwares = new List<SoftwareTable>();
        }
 
        [Column("SoftwareFileId")] 
		public System.Guid Id { get; set; }
 
        [Column("FileName")] 
		public string FileName { get; set; }
 
        [Column("Binary")] 
		public byte[] Binary { get; set; }

        public virtual ICollection<SoftwareTable> Softwares { get; set; }

    }
}
