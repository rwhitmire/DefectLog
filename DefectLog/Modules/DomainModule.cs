using System.Web.Http;
using DefectLog.Domain;
using Ninject.Modules;
using Ninject.Web.Common;

namespace DefectLog.Modules
{
    [Authorize]
    public class DomainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IContextFactory>().To<LogContextFactory>().InRequestScope();
            Bind(typeof (IRepository<>)).To(typeof (Repository<>));
        }
    }
}