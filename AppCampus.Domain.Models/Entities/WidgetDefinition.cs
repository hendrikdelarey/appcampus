using AppCampus.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.Entities
{
    public class WidgetDefinition : DomainEntity<Guid>, IAggregateRoot
    {
        public string Name { get; private set; }

        public string AssemblyType{ get; private set; }

        private readonly List<ParameterDefinition> parameterDefinitions;
        public IReadOnlyCollection<ParameterDefinition> ParameterDefinitions 
        { 
            get 
            {
                return parameterDefinitions;
            } 
        }

        // Create
        public WidgetDefinition(string name)
            : base(CombIdentityFactory.GenerateIdentity())
        {
            parameterDefinitions = new List<ParameterDefinition>();
            Name = name;
            AssemblyType = "Temp";
        }

        // Hydrate
        public WidgetDefinition(Guid id, string name)
            : base(id)
        {
            parameterDefinitions = new List<ParameterDefinition>();
            Name = name;
            AssemblyType = "Temp";
        }

        public void AddParameterDefinition(ParameterDefinition parameter) 
        {
            parameterDefinitions.Add(parameter);
        }

        public void RemoveParameterDefinition(ParameterDefinition parameter)
        {
            if (!parameterDefinitions.Contains(parameter)) 
            {
                throw new ArgumentOutOfRangeException("parameter", "The parameter you are trying to remove does not exist in the list.");
            }

            parameterDefinitions.Remove(parameter);
        }
    }
}
