using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("Slideshow")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class SlideshowTable : IDataEntity<Guid>
    {
        public SlideshowTable()
        {
            this.SignboardSlideshows = new List<SignboardSlideshowTable>();
            this.Slides = new List<SlideTable>();
        }
 
        [Column("SlideshowId")] 
		public System.Guid Id { get; set; }
 
        [Column("CompanyId")] 
		public System.Guid CompanyId { get; set; }
 
        [Column("Name")] 
		public string Name { get; set; }
 
        [Column("CreatedDate")] 
		public System.DateTime CreatedDate { get; set; }
 
        [Column("IsDeleted")] 
		public bool IsDeleted { get; set; }
        public virtual CompanyTable Company { get; set; }


        public virtual ICollection<SignboardSlideshowTable> SignboardSlideshows { get; set; }


        public virtual ICollection<SlideTable> Slides { get; set; }

    }
}
