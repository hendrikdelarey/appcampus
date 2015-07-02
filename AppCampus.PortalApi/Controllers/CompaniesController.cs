using AppCampus.Domain.Interfaces.Components;
using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.Events;
using AppCampus.PortalApi.Filters;
using AppCampus.PortalApi.Models.InputModels;
using AppCampus.PortalApi.Models.ResponseModels;
using System;
using System.Linq;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Description;
using Drumble.DomainDrivenArchitecture.Events;
using Drumble.Logging.WebApi.Filters;

namespace AppCampus.PortalApi.Controllers
{
    /// <summary>
    /// Companies represent the multitenant capability of the product.
    /// </summary>
    [RoutePrefix("api/v1/companies")]
    public class CompaniesController : ApiController
    {
        protected ICompanyRepository CompanyRepository { get; private set; }

        protected ILoggingComponent LoggingComponent { get; private set; }

        public CompaniesController(ICompanyRepository companyRepository, ILoggingComponent loggingComponent)
        {
            CompanyRepository = companyRepository;
            LoggingComponent = loggingComponent;
        }

        /// <summary>
        /// Retrieves a company.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <returns>The company model.</returns>
        [Route("{companyId:companyId}", Name = "GetCompany")]
        [ResponseType(typeof(CompanyModel))]
        [AuthoriseRoles(RoleClassification.GeneralUser)]
        public IHttpActionResult Get(Guid companyId)
        {
            var company = CompanyRepository.Find(companyId);

            if (company == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(CompanyModel.From(company));
            }
        }

        /// <summary>
        /// Lists all companies.
        /// </summary>
        /// <returns>A list of company models.</returns>
        [Route]
        [ResponseType(typeof(CompanyModel))]
        [AuthoriseRoles(RoleClassification.GeneralUser)]
        public IHttpActionResult Get()
        {
            var companies = CompanyRepository.GetAll();

            var response = companies.Select(x => CompanyModel.From(x));

            return Ok(response);
        }

        /// <summary>
        /// Creates a company.
        /// </summary>
        /// <param name="model">The company input model.</param>
        /// <returns>The created company.</returns>
        [Route]
        [ResponseType(typeof(CompanyModel))]
        [AuthoriseRoles(RoleClassification.SuperAdministrator)]
        public IHttpActionResult Post(CompanyInputModel model)
        {
            var company = new Company(model.Name);

            CompanyRepository.Add(company);

            var response = CompanyModel.From(company);

            return Created(new Uri(Url.Link("GetCompany", new { companyId = company.Id })), response);
        }
    }
}