using AppCampus.Domain.Interfaces.Components;
using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Events;
using AppCampus.Domain.Models.Identity;
using AppCampus.Infrastructure.Components;
using AppCampus.Infrastructure.Events;
using AppCampus.Infrastructure.Models;
using AppCampus.Infrastructure.Modules.Authentication;
using AppCampus.Infrastructure.Repositories.AnnouncementAggregate;
using AppCampus.Infrastructure.Repositories.CompanyAggregate;
using AppCampus.Infrastructure.Repositories.DeviceAggregate;
using AppCampus.Infrastructure.Repositories.DeviceLogAggregate;
using AppCampus.Infrastructure.Repositories.SignboardAggregate;
using AppCampus.Infrastructure.Repositories.SlideshowAggregate;
using AppCampus.Infrastructure.Repositories.SoftwareAggregate;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;
using System;
using Drumble.DomainDrivenArchitecture.Infrastructure.Resolution;

namespace AppCampus.Resolution
{
    public class Resolver
    {
        public IUnityContainer RegisterTypes(IUnityContainer container)
        {
            var aggregates = new AggregateResolver<AppCampusContext>(container);
            var services = new ServiceResolver(container);
            var events = new EventResolver(container);

            aggregates.RegisterAggregate<Device, DeviceTable, DeviceMapper, IDeviceRepository, DeviceRepository>();
            aggregates.RegisterAggregate<Company, CompanyTable, CompanyMapper, ICompanyRepository, CompanyRepository>();
            aggregates.RegisterAggregate<Slideshow, SlideshowTable, SlideshowMapper, ISlideshowRepository, SlideshowRepository>();
            aggregates.RegisterAggregate<Signboard, SignboardTable, SignboardMapper, ISignboardRepository, SignboardRepository>();
            aggregates.RegisterAggregate<WidgetDefinition, WidgetDefinitionTable, WidgetDefinitionMapper, IWidgetDefinitionRepository, WidgetDefinitionRepository>();
            aggregates.RegisterAggregate<Announcement, AnnouncementTable, AnnouncementMapper, IAnnouncementRepository, AnnouncementRepository>();
            aggregates.RegisterAggregate<Software, SoftwareTable, SoftwareMapper, ISoftwareRepository, SoftwareRepository>();
            aggregates.RegisterAggregate<DeviceLog, DeviceLogTable, DeviceLogMapper, IDeviceLogRepository, DeviceLogRepository>();

            services.RegisterService<IImageComponent, ImageComponent>();
            services.RegisterService<IDiagnosticsComponent, DiagnosticsComponent>();
            services.RegisterService<ITimetableComponent, TimetableComponent>();
            services.RegisterService<IDrumbleComponent, DrumbleComponent>();
            services.RegisterService<ILoggingComponent, LoggingComponent>();
            services.RegisterService<IScreenshotComponent, ScreenshotComponent>();

            services.RegisterService<IRoleStore<ApplicationRole, Guid>, RoleStore>();
            services.RegisterService<IUserStore<ApplicationUser, Guid>, UserStore>();

            events.RegisterEvent<PasswordChanged, EmailPasswordChange>();
            events.RegisterEvent<UserCreated, EmailNewUser>();

            return container;
        }
    }
}