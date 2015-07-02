using AppCampus.Signboard.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Models.ResponseModels
{
    public class RequestModel
    {
        public Guid RequestId { get; set; }

        public RequestType RequestType { get; set; }

        public string Value { get; set; }

        public string ValueType { get; set; }
    }
}
