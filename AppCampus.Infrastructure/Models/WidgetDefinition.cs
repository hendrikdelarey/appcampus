using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("WidgetDefinition")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class WidgetDefinitionTable : IDataEntity<Guid>
    {
        public WidgetDefinitionTable()
        {
            this.ParameterDefinitions = new List<ParameterDefinitionTable>();
            this.SlideWidgets = new List<SlideWidgetTable>();
        }
 
        [Column("WidgetDefinitionId")] 
		public System.Guid Id { get; set; }
 
        [Column("Name")] 
		public string Name { get; set; }
 
        [Column("AssemblyType")] 
		public string AssemblyType { get; set; }

        public virtual ICollection<ParameterDefinitionTable> ParameterDefinitions { get; set; }


        public virtual ICollection<SlideWidgetTable> SlideWidgets { get; set; }

    }
}
