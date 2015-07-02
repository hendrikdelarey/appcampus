using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppCampus.PortalApi.Providers
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        protected UserManager<ApplicationUser, Guid> UserManager { get; private set; }

        public SimpleAuthorizationServerProvider(UserManager<ApplicationUser, Guid> userManager)
        {
            UserManager = userManager;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Factory.StartNew(() => context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var user = await UserManager.FindAsync(context.UserName, context.Password);
            var data = await context.Request.ReadFormAsync();

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("userId", user.Id.ToString()));

            foreach (var role in UserManager.GetRoles(user.Id))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
            }

            foreach (var claim in UserManager.GetClaims(user.Id))
            {
                identity.AddClaim(new Claim(claim.Type, claim.Value));
            }

            var isSuperAdmin = identity.HasClaim(ClaimTypes.Role, RoleClassification.SuperAdministrator.ToString());
            Guid cloakCompanyId;
            Guid companyId;

            if (data["cloakCompanyId"] != null && Guid.TryParse(data["cloakCompanyId"], out cloakCompanyId) && isSuperAdmin)
            {
                // Super administrator cloaking of a company.
                companyId = cloakCompanyId;
            }
            else
            {
                // Typical user sign in.
                companyId = user.CompanyId;
            }

            identity.AddClaim(new Claim("companyId", companyId.ToString()));
            identity.AddClaim(new Claim("isSuper", isSuperAdmin.ToString()));

            var properties = CreateProperties(user.Id, companyId, isSuperAdmin);
            var ticket = new AuthenticationTicket(identity, properties);

            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return base.TokenEndpoint(context);
        }

        public static AuthenticationProperties CreateProperties(Guid userId, Guid companyId, bool isSuper)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                {
                    "userId", userId.ToString()
                },
                {
                    "companyId", companyId.ToString()
                },
                {
                    "isSuper", isSuper.ToString()
                }
            };
            return new AuthenticationProperties(data);
        }
    }
}