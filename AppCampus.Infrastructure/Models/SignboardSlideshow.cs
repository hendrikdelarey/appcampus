using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("SignboardSlideshow")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class SignboardSlideshowTable : IDataEntity<Guid>
    {
 
        [Column("SignboardSlideshowId")] 
		public System.Guid Id { get; set; }
 
        [Column("SignboardId")] 
		public System.Guid SignboardId { get; set; }
 
        [Column("SlideshowId")] 
		public System.Guid SlideshowId { get; set; }
 
        [Column("StartDate")] 
		public System.DateTime StartDate { get; set; }
 
        [Column("CreatedDate")] 
		public System.DateTime CreatedDate { get; set; }
 
        [Column("IsActive")] 
		public bool IsActive { get; set; }
        public virtual SignboardTable Signboard { get; set; }

        public virtual SlideshowTable Slideshow { get; set; }

    }
}
