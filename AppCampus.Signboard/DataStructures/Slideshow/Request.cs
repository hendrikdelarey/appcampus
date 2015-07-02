using AppCampus.Signboard.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.DataStructures.Slideshow
{
    public class Request
    {
        private Request(Guid id, RequestType type, string value) 
        {
            Id = id;
            RequestType = type;
            Value = value;
        }

        public Guid Id { get; set; }

        public RequestType RequestType { get; set; }

        public string Value { get; set; }

        public static Request From(Guid id, RequestType type, string value, string valueType) 
        {
            return new Request(id, type, value);
        }

        public Request Copy() 
        {
            return new Request(Id, RequestType, Value);
        }
    }
}
