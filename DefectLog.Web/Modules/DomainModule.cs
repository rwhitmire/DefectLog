using DefectLog.Core.Domain;
using Ninject.Modules;
using Ninject.Web.Common;

namespace DefectLog.Web.Modules
{
    public class DomainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IContextFactory>().To<LogContextFactory>().InRequestScope();
            Bind(typeof (IRepository<>)).To(typeof (Repository<>));
        }
    }
}