using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.Identity;
using AppCampus.PortalApi.Filters;
using AppCampus.PortalApi.Models.ResponseModels;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace AppCampus.PortalApi.Controllers
{
    [RoutePrefix("api/v1/roles")]
    [AuthoriseRoles(RoleClassification.Administrator)]
    public class RolesController : ApiController
    {
        protected RoleManager<ApplicationRole, Guid> RoleManager { get; private set; }

        public RolesController(IRoleStore<ApplicationRole, Guid> roleStore)
        {
            RoleManager = new RoleManager<ApplicationRole, Guid>(roleStore);
        }

        /// <summary>
        /// Retrieves a role.
        /// </summary>
        /// <param name="roleId">The identifier of the role.</param>
        /// <returns>The role model.</returns>
        [Route("{roleId:Guid}", Name = "GetRole")]
        [ResponseType(typeof(RoleModel))]
        public IHttpActionResult Get(Guid roleId)
        {
            var role = RoleManager.FindById(roleId);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(RoleModel.From(role));
        }

        /// <summary>
        /// Lists all roles.
        /// </summary>
        /// <returns>A list of role models.</returns>
        [Route]
        [ResponseType(typeof(RoleModel))]
        public IHttpActionResult Get()
        {
            var roles = RoleManager.Roles.ToList();

            return Ok(roles.Select(RoleModel.From));
        }
    }
}