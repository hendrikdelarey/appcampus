using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("Parameter")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class ParameterTable : IDataEntity<Guid>
    {
 
        [Column("ParameterId")] 
		public System.Guid Id { get; set; }
 
        [Column("ParameterDefinitionId")] 
		public System.Guid ParameterDefinitionId { get; set; }
 
        [Column("Value")] 
		public string Value { get; set; }
 
        [Column("SlideWidgetId")] 
		public Nullable<System.Guid> SlideWidgetId { get; set; }
        public virtual ParameterDefinitionTable ParameterDefinition { get; set; }

        public virtual SlideWidgetTable SlideWidget { get; set; }

    }
}
