using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using System;

namespace AppCampus.SignboardApi.Models.ResponseModels
{
    public class RequestResponseModel
    {
        public Guid RequestId { get; set; }
        public RequestType RequestType { get; set; }
        public string Value { get; set; }
        public string ValueType { get; set; }

        public static RequestResponseModel From(Request request) 
        {
            return new RequestResponseModel()
            {
                RequestId = request.Id,
                RequestType = request.RequestType,
                Value = request.Value,
                ValueType = request.ValueType.ToString()
            };
        }
    }
}