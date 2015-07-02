using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("Widget")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class WidgetTable : IDataEntity<Guid>
    {
        public WidgetTable()
        {
            this.Parameters = new List<ParameterTable>();
            this.SlideWidgets = new List<SlideWidgetTable>();
        }
 
        [Column("WidgetId")] 
		public System.Guid Id { get; set; }
 
        [Column("Name")] 
		public string Name { get; set; }
 
        [Column("AssemblyType")] 
		public string AssemblyType { get; set; }

        public virtual ICollection<ParameterTable> Parameters { get; set; }


        public virtual ICollection<SlideWidgetTable> SlideWidgets { get; set; }

    }
}
