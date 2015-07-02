using AppCampus.SignboardApi.Models.ResponseModels;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace AppCampus.SignboardApi.Extensions
{
    public class CustomApiController : ApiController
    {
        public IHttpActionResult NoContent()
        {
            return StatusCode(HttpStatusCode.NoContent);
        }

        public IHttpActionResult UnsupporteMediaType()
        {
            return new ResponseMessageResult(Request.CreateResponse(
                    HttpStatusCode.UnsupportedMediaType,
                    ErrorModel.FromUnsupportedMediaType()
                )
            );
        }

        public new IHttpActionResult NotFound()
        {
            return new ResponseMessageResult(Request.CreateResponse(
                   HttpStatusCode.NotFound,
                   ErrorModel.FromNotFound()
               )
           );
        }
    }
}