using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppCampus.SignboardApi.Models.InputModels
{
    public class RequestInputModel
    {
        public bool Success { get; set; }
        public Guid RequestId { get; set; }
    }
}