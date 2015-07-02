using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.Identity;
using AppCampus.PortalApi.Providers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace AppCampus.PortalApi
{
    public partial class Startup
    {
        public UserManager<ApplicationUser, Guid> UserManager { get; private set; }

        public RoleManager<ApplicationRole, Guid> RoleManager { get; private set; }

        public ICompanyRepository CompanyRepository { get; private set; }

        public IWidgetDefinitionRepository WidgetDefinitionRepository { get; private set; }

        public void ConfigureAuth(IAppBuilder app, HttpConfiguration config)
        {
            var userStore = config.DependencyResolver.GetService(typeof(IUserStore<ApplicationUser, Guid>)) as IUserStore<ApplicationUser, Guid>;
            var roleStore = config.DependencyResolver.GetService(typeof(IRoleStore<ApplicationRole, Guid>)) as IRoleStore<ApplicationRole, Guid>;

            UserManager = new UserManager<ApplicationUser, Guid>(userStore);
            RoleManager = new RoleManager<ApplicationRole, Guid>(roleStore);

            CompanyRepository = config.DependencyResolver.GetService(typeof(ICompanyRepository)) as ICompanyRepository;
            WidgetDefinitionRepository = config.DependencyResolver.GetService(typeof(IWidgetDefinitionRepository)) as IWidgetDefinitionRepository;

            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/v1/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider(UserManager)
            };

            // Adds OAuth server (token generation)
            app.UseOAuthAuthorizationServer(oAuthServerOptions);

            // Adds bearer token processing to pipeline (token consumption)
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            Seed();
        }

        private void Seed()
        {
            CreateRoleIfNotExists(RoleClassification.GeneralUser);
            CreateRoleIfNotExists(RoleClassification.DeviceManager);
            CreateRoleIfNotExists(RoleClassification.AnnouncementAuthor);
            CreateRoleIfNotExists(RoleClassification.SlideshowAuthor);
            CreateRoleIfNotExists(RoleClassification.Administrator);

            var company = CreateCompanyIfNotExists(new Company("DrumbleDore"));

            CreateSuperAdminIfNotExists(new ApplicationUser(company.Id, "support@Drumble.co.za", "DrumbleDore", "Production"));

            CreateTimetableWidgetDefinitionIfNotExists();
            CreateImageWidgetDefinitionIfNotExists();

            CreateTextWidgetDefinition();
        }

        private void CreateTextWidgetDefinition()
        {
            var widgetDefinition = WidgetDefinitionRepository.FindByName("Text");

            ParameterDefinition textParameterDefinition = new ParameterDefinition("Text");
            ParameterDefinition fontSizeParameterDefinition = new ParameterDefinition("FontSize");
            ParameterDefinition textColourParameterDefinition = new ParameterDefinition("TextColour");

            if (widgetDefinition == null)
            {
                widgetDefinition = new WidgetDefinition("Text");
                widgetDefinition.AddParameterDefinition(textParameterDefinition);
                widgetDefinition.AddParameterDefinition(fontSizeParameterDefinition);
                widgetDefinition.AddParameterDefinition(textColourParameterDefinition);

                WidgetDefinitionRepository.Add(widgetDefinition);
            }
            else
            {
                if (!widgetDefinition.ParameterDefinitions.Any(x => x.Name == "Text"))
                {
                    widgetDefinition.AddParameterDefinition(textParameterDefinition);
                    WidgetDefinitionRepository.Update(widgetDefinition);
                }

                if (!widgetDefinition.ParameterDefinitions.Any(x => x.Name == "FontSize"))
                {
                    widgetDefinition.AddParameterDefinition(fontSizeParameterDefinition);
                    WidgetDefinitionRepository.Update(widgetDefinition);
                }
                if (!widgetDefinition.ParameterDefinitions.Any(x => x.Name == "TextColour"))
                {
                    widgetDefinition.AddParameterDefinition(textColourParameterDefinition);
                    WidgetDefinitionRepository.Update(widgetDefinition);
                }
            }
        }

        private void CreateTimetableWidgetDefinitionIfNotExists()
        {
            var widgetDefinition = WidgetDefinitionRepository.FindByName("Timetable");

            ParameterDefinition pollingIntervalParameterDefinition = new ParameterDefinition("PollingIntervalInSeconds", "30");
            ParameterDefinition stopIdentifierParameterDefinition = new ParameterDefinition("StopIdentifier");
            ParameterDefinition operatorNameParameterDefinition = new ParameterDefinition("OperatorName");
            ParameterDefinition numResultsParameterDefinition = new ParameterDefinition("NumResults", "6");

            ParameterDefinition walkingDistanceToStopParameterDefinition = new ParameterDefinition("WalkingDistanceToStopInSeconds", "1");
            ParameterDefinition operatorDisplayNameParameterfinition = new ParameterDefinition("OperatorDisplayName", "");

            if (widgetDefinition == null)
            {
                widgetDefinition = new WidgetDefinition("Timetable");

                widgetDefinition.AddParameterDefinition(pollingIntervalParameterDefinition);
                widgetDefinition.AddParameterDefinition(stopIdentifierParameterDefinition);
                widgetDefinition.AddParameterDefinition(operatorNameParameterDefinition);
                widgetDefinition.AddParameterDefinition(numResultsParameterDefinition);

                widgetDefinition.AddParameterDefinition(walkingDistanceToStopParameterDefinition);
                widgetDefinition.AddParameterDefinition(operatorDisplayNameParameterfinition);

                WidgetDefinitionRepository.Add(widgetDefinition);
            }
            else
            {
                bool updateNeeded = false;
                if (!widgetDefinition.ParameterDefinitions.Any(x => x.Name == "PollingIntervalInSeconds"))
                {
                    widgetDefinition.AddParameterDefinition(pollingIntervalParameterDefinition);
                    updateNeeded = true;
                }

                if (!widgetDefinition.ParameterDefinitions.Any(x => x.Name == "StopIdentifier"))
                {
                    widgetDefinition.AddParameterDefinition(stopIdentifierParameterDefinition);
                    updateNeeded = true;
                }

                if (!widgetDefinition.ParameterDefinitions.Any(x => x.Name == "OperatorName"))
                {
                    widgetDefinition.AddParameterDefinition(operatorNameParameterDefinition);
                    updateNeeded = true;
                }

                if (!widgetDefinition.ParameterDefinitions.Any(x => x.Name == "NumResults"))
                {
                    widgetDefinition.AddParameterDefinition(numResultsParameterDefinition);
                    updateNeeded = true;
                }

                if (!widgetDefinition.ParameterDefinitions.Any(x => x.Name == "WalkingDistanceToStopInSeconds"))
                {
                    widgetDefinition.AddParameterDefinition(walkingDistanceToStopParameterDefinition);
                    updateNeeded = true;
                }

                if (!widgetDefinition.ParameterDefinitions.Any(x => x.Name == "OperatorDisplayName"))
                {
                    widgetDefinition.AddParameterDefinition(operatorDisplayNameParameterfinition);
                    updateNeeded = true;
                }

                if (updateNeeded)
                {
                    WidgetDefinitionRepository.Update(widgetDefinition);
                }
            }
        }

        private void CreateImageWidgetDefinitionIfNotExists()
        {
            var widgetDefinition = WidgetDefinitionRepository.FindByName("Image");

            ParameterDefinition imageIdentifierParameterDefinition = new ParameterDefinition("ImageIdentifier");
            ParameterDefinition iamgeFillParameterDefinition = new ParameterDefinition("ImageFill", "Scale");

            if (widgetDefinition == null)
            {
                widgetDefinition = new WidgetDefinition("Image");
                widgetDefinition.AddParameterDefinition(imageIdentifierParameterDefinition);

                WidgetDefinitionRepository.Add(widgetDefinition);
            }
            else
            {
                if (!widgetDefinition.ParameterDefinitions.Any(x => x.Name == "ImageIdentifier"))
                {
                    widgetDefinition.AddParameterDefinition(imageIdentifierParameterDefinition);
                    WidgetDefinitionRepository.Update(widgetDefinition);
                }

                if (!widgetDefinition.ParameterDefinitions.Any(x => x.Name == "ImageFill"))
                {
                    widgetDefinition.AddParameterDefinition(iamgeFillParameterDefinition);
                    WidgetDefinitionRepository.Update(widgetDefinition);
                }
            }
        }

        private void CreateRoleIfNotExists(RoleClassification roleClass)
        {
            if (!RoleManager.RoleExists(roleClass.ToString()))
            {
                var role = new ApplicationRole(Guid.NewGuid(), roleClass.ToString());
                RoleManager.Create(role);
            }
        }

        private Company CreateCompanyIfNotExists(Company company)
        {
            var existingCompany = CompanyRepository.FindByName(company.Name);

            if (existingCompany == null)
            {
                CompanyRepository.Add(company);
                return company;
            }
            else
            {
                return existingCompany;
            }
        }

        private void CreateSuperAdminIfNotExists(ApplicationUser newUser)
        {
            var user = UserManager.FindByName(newUser.UserName);

            if (user == null)
            {
                UserManager.Create(newUser, "Drumble85!");
                user = newUser;
            }

            UserManager.AddToRole(user.Id, RoleClassification.GeneralUser.ToString());
            UserManager.AddToRole(user.Id, RoleClassification.DeviceManager.ToString());
            UserManager.AddToRole(user.Id, RoleClassification.AnnouncementAuthor.ToString());
            UserManager.AddToRole(user.Id, RoleClassification.SlideshowAuthor.ToString());
            UserManager.AddToRole(user.Id, RoleClassification.Administrator.ToString());

            var claim = UserManager.GetClaims(user.Id).SingleOrDefault(c => c.Type == ClaimTypes.Role && c.Value == "SuperAdministrator");

            if (claim == null)
            {
                UserManager.AddClaim(user.Id, new Claim(ClaimTypes.Role, "SuperAdministrator"));
            }
        }
    }
}