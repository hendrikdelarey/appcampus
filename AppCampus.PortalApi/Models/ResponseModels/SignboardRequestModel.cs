using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using System;

namespace AppCampus.PortalApi.Models.ResponseModels
{
    public class SignboardRequestModel
    {
        /// <summary>
        /// The identifier of the request.
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        /// The type of the request.
        /// </summary>
        public RequestType RequestType { get; set; }

        /// <summary>
        /// The value if the request needs a value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The state of the request.
        /// </summary>
        public RequestState State { get; set; }

        /// <summary>
        /// The created date of the request.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        public static SignboardRequestModel From(Request request)
        {
            return new SignboardRequestModel()
            {
                RequestType = request.RequestType,
                Value = request.Value,
                RequestId = request.Id,
                State = request.State,
                CreatedDate = request.CreatedDate
            };
        }
    }
}