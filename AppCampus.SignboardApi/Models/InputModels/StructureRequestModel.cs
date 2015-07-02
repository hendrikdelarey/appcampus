using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppCampus.SignboardApi.Models.QueryModels
{
    public class StructureRequestModel
    {
        public string MacAddress { get; set; }
        public Guid SlideshowId { get; set; }
    }
}