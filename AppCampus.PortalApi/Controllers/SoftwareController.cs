using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
using AppCampus.PortalApi.Extensions;
using AppCampus.PortalApi.Filters;
using AppCampus.PortalApi.Models.InputModels;
using AppCampus.PortalApi.Models.ResponseModels;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AppCampus.PortalApi.Controllers
{
    [RoutePrefix("api/v1/software")]
    public class SoftwareController : CustomApiController
    {
        public ISoftwareRepository SoftwareRepository { get; private set; }

        public SoftwareController(ISoftwareRepository softwareRepository)
        {
            SoftwareRepository = softwareRepository;
        }

        /// <summary>
        /// Retrieves a software version.
        /// </summary>
        /// <param name="softwareId">The identifier of the software version.</param>
        /// <returns>The software version model.</returns>
        [Route("{softwareId:Guid}", Name = "GetSoftware")]
        [AuthoriseRoles(RoleClassification.GeneralUser)]
        [ResponseType(typeof(SoftwareModel))]
        public IHttpActionResult Get(Guid softwareId)
        {
            var software = SoftwareRepository.Find(softwareId);

            if (software == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(SoftwareModel.From(software));
            }
        }

        /// <summary>
        /// Lists all software versions.
        /// </summary>
        /// <returns>A list of software version models.</returns>
        [Route]
        [AuthoriseRoles(RoleClassification.GeneralUser)]
        [ResponseType(typeof(SoftwareModel))]
        public IHttpActionResult Get()
        {
            var software = SoftwareRepository.GetAll();

            var response = software.Select(x => SoftwareModel.From(x));

            return Ok(response);
        }

        /// <summary>
        /// Creates a software version.
        /// </summary>
        /// <param name="model">The software version input model.</param>
        /// <returns>The created software version.</returns>
        [Route]
        [AuthoriseRoles(RoleClassification.SuperAdministrator)]
        [ResponseType(typeof(SoftwareModel))]
        public IHttpActionResult Post(SoftwareInputModel model)
        {
            var software = new Software(Version.Parse(model.Version), model.Comment);

            SoftwareRepository.Add(software);

            var response = SoftwareModel.From(software);

            return Created(new Uri(Url.Link("GetSoftware", new { softwareId = software.Id })), response);
        }

        /// <summary>
        /// Get a file for a specific software version.
        /// </summary>
        /// <param name="softwareId">The identifier of the software version.</param>
        /// <returns>The file.</returns>
        [Route("{softwareId:Guid}/file", Name = "GetSoftwareFile")]
        [AllowAnonymous]
        [ResponseType(typeof(SoftwareModel))]
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

        /// <summary>
        /// Posts a file, using a MIME multipart (multipart/form-data) request, to a specific software version.
        /// </summary>
        /// <param name="softwareId">The identifier of the software version.</param>
        /// <returns></returns>
        [Route("{softwareId:Guid}/file")]
        [AuthoriseRoles(RoleClassification.SuperAdministrator)]
        [ResponseType(typeof(SoftwareModel))]
        public async Task<IHttpActionResult> PostFile(Guid softwareId)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return UnsupporteMediaType();
            }

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            if (!provider.Contents.Any() || provider.Contents.Count() > 1)
            {
                return BadRequest("At least one and only one file is supported.");
            }

            var file = provider.Contents.First();

            var fileName = file.Headers.ContentDisposition.FileName.Replace("\"", String.Empty);

            SoftwareRepository.UploadSoftware(softwareId, fileName, file.ReadAsByteArrayAsync().Result);

            return Created(new Uri(Url.Link("GetSoftwareFile", new { softwareId = softwareId })), String.Empty);
        }
    }
}