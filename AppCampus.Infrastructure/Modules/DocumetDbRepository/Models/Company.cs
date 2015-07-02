using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCampus.Infrastructure.Modules.DocumetDbRepository.Models
{
    public class Company
    {
        [JsonProperty(PropertyName = "CompanyId")]
        public string CompanyId { get; set; }

        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
    }
}
