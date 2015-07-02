using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCampus.Infrastructure.Models
{
    [Table("ParameterDefinition")]
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class ParameterDefinitionTable : IDataEntity<Guid>
    {
        public ParameterDefinitionTable()
        {
            this.Parameters = new List<ParameterTable>();
        }
 
        [Column("ParameterDefinitionId")] 
		public System.Guid Id { get; set; }
 
        [Column("WidgetDefinitionId")] 
		public System.Guid WidgetDefinitionId { get; set; }
 
        [Column("Name")] 
		public string Name { get; set; }
 
        [Column("DefaultValue")] 
		public string DefaultValue { get; set; }
 
        [Column("ParameterType")] 
		public int ParameterType { get; set; }

        public virtual ICollection<ParameterTable> Parameters { get; set; }

        public virtual WidgetDefinitionTable WidgetDefinition { get; set; }

    }
}
