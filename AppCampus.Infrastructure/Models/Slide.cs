using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("Slide")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class SlideTable : IDataEntity<Guid>
    {
        public SlideTable()
        {
            this.SlideWidgets = new List<SlideWidgetTable>();
        }
 
        [Column("SlideId")] 
		public System.Guid Id { get; set; }
 
        [Column("SlideshowId")] 
		public System.Guid SlideshowId { get; set; }
 
        [Column("Duration")] 
		public int Duration { get; set; }
 
        [Column("Transition")] 
		public string Transition { get; set; }
 
        [Column("OrderNumber")] 
		public int OrderNumber { get; set; }
 
        [Column("IsDeleted")] 
		public bool IsDeleted { get; set; }
 
        [Column("IsActive")] 
		public bool IsActive { get; set; }
 
        [Column("Name")] 
		public string Name { get; set; }
 
        [Column("BackgroundColour")] 
		public string BackgroundColour { get; set; }
        public virtual SlideshowTable Slideshow { get; set; }


        public virtual ICollection<SlideWidgetTable> SlideWidgets { get; set; }

    }
}
