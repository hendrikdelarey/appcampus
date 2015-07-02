using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.SignboardApi.Extensions;
using AppCampus.SignboardApi.Models.InputModels;
using AppCampus.SignboardApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace AppCampus.SignboardApi.Controllers
{
    [RoutePrefix("signboardapi/v1/signboards/{macAddress}/logFiles")]
    public class LogFilesController : CustomApiController
    {
        private IDeviceLogRepository DeviceLogRepository { get; set; }

        public LogFilesController(IDeviceLogRepository deviceLogRepository) 
        {
            DeviceLogRepository = deviceLogRepository;
        }

        /// <summary>
        /// Creates a DeviceLog object.
        /// </summary>
        /// <param name="model">Device Log input model.</param>
        /// <returns>The created Device Log.</returns>
        [Route]
        [ResponseType(typeof(DeviceLogResponseModel))]
        public IHttpActionResult Post(DeviceLogInputModel model)
        {
            if (String.IsNullOrWhiteSpace(model.FileName)) 
            {
                return BadRequest();
            }

            var logFile = new DeviceLog(model.FileName);
            var fileId = DeviceLogRepository.CreateDeviceLog(logFile);
            var response = DeviceLogResponseModel.From(fileId);

            return Ok(response);
        }

        /// <summary>
        /// Posts a file, using a MIME multipart (multipart/form-data) request, to a specific software version.
        /// </summary>
        /// <param name="logFileId">The identifier of the log file.</param>
        /// <returns></returns>
        [Route("{logFileId:Guid}/file")]
        [ResponseType(typeof(DeviceLogResponseModel))]
        public async Task<IHttpActionResult> PostFile(Guid logFileId)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return UnsupporteMediaType();
            }

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            if (!provider.Contents.Any())
            {
                return BadRequest("At least one and only one file is supported.");
            }

            var file = provider.Contents.Last();

            var fileName = file.Headers.ContentDisposition.FileName.Replace("\"", String.Empty);

            DeviceLogRepository.UploadLogFile(logFileId, file.ReadAsByteArrayAsync().Result);

            return Ok();
        }
    }
}