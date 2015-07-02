using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.CodeDom.Compiler;
using Drumble.CachedConfigurationManager;
using Drumble.DomainDrivenArchitecture.Domain.EntityFramework;
using AppCampus.Infrastructure.Models.Mapping;

namespace AppCampus.Infrastructure.Models
{
	[GeneratedCode("EntityFrameworkCodeGeneration", "6.1.1")]
    public partial class AppCampusContext : EntityFrameworkContext
    {
        static AppCampusContext()
        {
            Database.SetInitializer<AppCampusContext>(null);
        }

        public AppCampusContext()
			: base(CachedConfigurationManager.Instance.GetSetting<string>("AppCampusContext"))
        {
        }

        public DbSet<AnnouncementTable> Announcements { get; set; }
        public DbSet<CompanyTable> Companies { get; set; }
        public DbSet<DeviceTable> Devices { get; set; }
        public DbSet<DeviceLogTable> DeviceLogs { get; set; }
        public DbSet<DeviceStateTable> DeviceStates { get; set; }
        public DbSet<LogFileTable> LogFiles { get; set; }
        public DbSet<ParameterTable> Parameters { get; set; }
        public DbSet<ParameterDefinitionTable> ParameterDefinitions { get; set; }
        public DbSet<ScreenshotTable> Screenshots { get; set; }
        public DbSet<SignboardTable> Signboards { get; set; }
        public DbSet<SignboardAnnouncementTable> SignboardAnnouncements { get; set; }
        public DbSet<SignboardRequestTable> SignboardRequests { get; set; }
        public DbSet<SignboardSlideshowTable> SignboardSlideshows { get; set; }
        public DbSet<SlideTable> Slides { get; set; }
        public DbSet<SlideshowTable> Slideshows { get; set; }
        public DbSet<SlideWidgetTable> SlideWidgets { get; set; }
        public DbSet<SoftwareTable> Softwares { get; set; }
        public DbSet<SoftwareFileTable> SoftwareFiles { get; set; }
        public DbSet<WidgetDefinitionTable> WidgetDefinitions { get; set; }
        public DbSet<RoleTable> Roles { get; set; }
        public DbSet<UserTable> Users { get; set; }
        public DbSet<UserClaimTable> UserClaims { get; set; }
        public DbSet<UserRoleTable> UserRoles { get; set; }
        public DbSet<ImageTable> Images { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AnnouncementTableMap());
            modelBuilder.Configurations.Add(new CompanyTableMap());
            modelBuilder.Configurations.Add(new DeviceTableMap());
            modelBuilder.Configurations.Add(new DeviceLogTableMap());
            modelBuilder.Configurations.Add(new DeviceStateTableMap());
            modelBuilder.Configurations.Add(new LogFileTableMap());
            modelBuilder.Configurations.Add(new ParameterTableMap());
            modelBuilder.Configurations.Add(new ParameterDefinitionTableMap());
            modelBuilder.Configurations.Add(new ScreenshotTableMap());
            modelBuilder.Configurations.Add(new SignboardTableMap());
            modelBuilder.Configurations.Add(new SignboardAnnouncementTableMap());
            modelBuilder.Configurations.Add(new SignboardRequestTableMap());
            modelBuilder.Configurations.Add(new SignboardSlideshowTableMap());
            modelBuilder.Configurations.Add(new SlideTableMap());
            modelBuilder.Configurations.Add(new SlideshowTableMap());
            modelBuilder.Configurations.Add(new SlideWidgetTableMap());
            modelBuilder.Configurations.Add(new SoftwareTableMap());
            modelBuilder.Configurations.Add(new SoftwareFileTableMap());
            modelBuilder.Configurations.Add(new WidgetDefinitionTableMap());
            modelBuilder.Configurations.Add(new RoleTableMap());
            modelBuilder.Configurations.Add(new UserTableMap());
            modelBuilder.Configurations.Add(new UserClaimTableMap());
            modelBuilder.Configurations.Add(new UserRoleTableMap());
            modelBuilder.Configurations.Add(new ImageTableMap());
        }
    }
}
