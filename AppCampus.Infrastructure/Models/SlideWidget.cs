using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("SlideWidget")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class SlideWidgetTable : IDataEntity<Guid>
    {
        public SlideWidgetTable()
        {
            this.Parameters = new List<ParameterTable>();
        }
 
        [Column("SlideWidgetId")] 
		public System.Guid Id { get; set; }
 
        [Column("SlideId")] 
		public System.Guid SlideId { get; set; }
 
        [Column("WidgetDefinitionId")] 
		public System.Guid WidgetDefinitionId { get; set; }

        public virtual ICollection<ParameterTable> Parameters { get; set; }

        public virtual SlideTable Slide { get; set; }

        public virtual WidgetDefinitionTable WidgetDefinition { get; set; }

    }
}
