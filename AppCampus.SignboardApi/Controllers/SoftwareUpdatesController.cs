using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.SignboardApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace AppCampus.SignboardApi.Controllers
{
    [RoutePrefix("signboardapi/v1/signboardsoftware")]
    public class SoftwareUpdatesController : ApiController
    {
        public ISoftwareRepository SoftwareRepository { get; set; }

        public SoftwareUpdatesController(ISoftwareRepository softwareRepository) 
        {
            SoftwareRepository = softwareRepository;
        }

        /// <summary>
        /// Get a file for a specific software version.
        /// </summary>
        /// <param name="softwareId">The identifier of the software version.</param>
        /// <returns>The file.</returns>
        [Route("{softwareId:Guid}/file", Name = "GetSoftwareFile")]
        public HttpResponseMessage GetFile(Guid softwareId)
        {
            var bytes = SoftwareRepository.DownloadSoftware(softwareId);

            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StreamContent(new MemoryStream(bytes));
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = SoftwareRepository.GetFileName(softwareId)
            };

            return response;
        }
    }
}