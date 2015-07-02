using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.PortalApi.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace AppCampus.PortalApi.Controllers
{
    [RoutePrefix("api/v1/companies/{companyId:Guid}/signboards/{signboardId:Guid}/logs")]
    public class SignboardLogsController : CustomApiController
    {
        private IDeviceLogRepository DeviceLogRepository { get; set; }

        public SignboardLogsController(IDeviceLogRepository deviceLogRepository) 
        {
            DeviceLogRepository = deviceLogRepository;
        }

        /// <summary>
        /// Get a file for a specific logId
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="signboardId">The identifier of the signboard.</param>
        /// <param name="logId">The identifier of the log.</param>
        /// <returns>The file.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "companyId"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "signboardId"), Route("{logId:Guid}", Name = "GetDeviceLogFile")]
        [AllowAnonymous]
        public HttpResponseMessage GetFile(Guid companyId, Guid signboardId, Guid logId)
        {
            var bytes = DeviceLogRepository.DownloadLogFile(logId);

            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StreamContent(new MemoryStream(bytes));
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = "screenshot"
            };

            return response;
        }
    }
}