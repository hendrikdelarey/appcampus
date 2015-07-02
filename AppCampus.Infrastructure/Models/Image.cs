using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("Image", Schema="widget")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class ImageTable : IDataEntity<Guid>
    {
 
        [Column("ImageId")] 
		public System.Guid Id { get; set; }
 
        [Column("Base64Image")] 
		public string Base64Image { get; set; }
 
        [Column("Name")] 
		public string Name { get; set; }
 
        [Column("CreatedDate")] 
		public System.DateTime CreatedDate { get; set; }
    }
}
