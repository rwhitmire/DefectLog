using DefectLog.Services;
using Ninject.Modules;

namespace DefectLog.Modules
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IVersionService>().To<VersionService>();
            Bind<IDefectService>().To<DefectService>();
            Bind<IUserService>().To<UserService>();
            Bind<IStatusService>().To<StatusService>();
            Bind<ICategoryService>().To<CategoryService>();
            Bind<IPriorityLevelService>().To<PriorityLevelService>();
            Bind<IEmailService>().To<EmailService>();
            Bind<IAppVersionService>().To<AppVersionService>();
            Bind<IRoleService>().To<RoleService>();
            Bind<ICryptographyService>().To<CryptographyService>();
            //Bind<IActiveDirectoryService>().To<ActiveDirectoryService>();
        }
    }
}