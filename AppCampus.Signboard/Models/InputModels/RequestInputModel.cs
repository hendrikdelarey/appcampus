using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Models.InputModels
{
    public class RequestInputModel
    {
        public bool Success { get; set; }
        public Guid RequestId { get; set; }

        public static RequestInputModel From(Guid requestId, bool success) 
        {
            return new RequestInputModel()
            {
                Success = success,
                RequestId = requestId
            };
        }
    }
}
