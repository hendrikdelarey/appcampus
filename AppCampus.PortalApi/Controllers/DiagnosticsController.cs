using AppCampus.Domain.Interfaces.Components;
using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Enums;
using AppCampus.PortalApi.Filters;
using AppCampus.PortalApi.Models.ResponseModels;
using System;
using System.Linq;
using System.Security.Permissions;
using System.Web.Http;
using System.Web.Http.Description;

namespace AppCampus.PortalApi.Controllers
{
    /// <summary>
    /// Signboards represent devices which are to be used to display information (slideshows).
    /// </summary>
    [RoutePrefix("api/v1/companies/{companyId:companyId}/signboards/{signboardId:Guid}/diagnostics")]
    [AuthoriseCompany]
    [AuthoriseRoles(RoleClassification.GeneralUser)]
    public class DiagnosticsController : ApiController
    {
        private ISignboardRepository SignboardRepository { get; set; }

        private IDiagnosticsComponent DiagnosticsComponent { get; set; }

        public DiagnosticsController(ISignboardRepository signboardRepository, IDiagnosticsComponent diagnosticsComponent)
        {
            SignboardRepository = signboardRepository;
            DiagnosticsComponent = diagnosticsComponent;
        }

        /// <summary>
        /// Retrieves the latest of a signboard's diagnostics.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="signboardId">The identifier of the company's signboard.</param>
        /// <returns>The signboard model.</returns>
        [Route("latest")]
        [ResponseType(typeof(SignboardDiagnosticModel))]
        public IHttpActionResult Get(Guid companyId, Guid signboardId)
        {
            var signboard = SignboardRepository.Find(signboardId);

            if(signboard == null)
            {
                return NotFound();
            }

            if(signboard.CompanyId != companyId)
            {
                return NotFound();
            }

            var diagnostics = DiagnosticsComponent.GetLatest(signboardId);

            if (diagnostics == null)
            {
                return Ok();
            }

            return Ok(SignboardDiagnosticModel.From(diagnostics));
        }

        /// <summary>
        /// Retrieves a signboard's diagnostics.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="signboardId">The identifier of the company's signboard.</param>
        /// <param name="atDate">The date from which to get the diagnostics.</param>
        /// <param name="take">The number of recorsd to retrieve.</param>
        /// <returns>The signboard model.</returns>
        [Route]
        [ResponseType(typeof(SignboardDiagnosticModel))]
        public IHttpActionResult Get(Guid companyId, Guid signboardId, DateTime atDate, int take = 10)
        {
            var signboard = SignboardRepository.Find(signboardId);

            if (signboard == null)
            {
                return NotFound();
            }

            if (signboard.CompanyId != companyId)
            {
                return NotFound();
            }

            var diagnostics = DiagnosticsComponent.GetFrom(signboardId, atDate, take);

            return Ok(diagnostics.Select(SignboardDiagnosticModel.From));
        }
    }
}