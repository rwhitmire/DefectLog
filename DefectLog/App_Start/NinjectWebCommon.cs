[assembly: WebActivator.PreApplicationStartMethod(typeof(DefectLog.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(DefectLog.App_Start.NinjectWebCommon), "Stop")]

namespace DefectLog.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Parameters;
    using Ninject.Syntax;
    using Ninject.Web.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Dependencies;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            
            RegisterServices(kernel);
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);

            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Load(Assembly.GetExecutingAssembly());
        }        
    }

    /// <summary>
    /// resolves dependencies for WebAPI
    /// </summary>
    internal class NinjectResolver : DependencyScope, IDependencyResolver
    {
        private readonly IKernel _resolutionRoot;

        public NinjectResolver(IKernel resolutionRoot)
            : base(resolutionRoot)
        {
            _resolutionRoot = resolutionRoot;
        }

        public IDependencyScope BeginScope()
        {
            return new DependencyScope(_resolutionRoot.BeginBlock());
        }
    }

    internal class DependencyScope : IDependencyScope
    {
        private readonly IResolutionRoot _resolutionRoot;

        public DependencyScope(IResolutionRoot resolutionRoot)
        {
            _resolutionRoot = resolutionRoot;
        }

        public void Dispose()
        {
            var disposable = _resolutionRoot as IDisposable;
            if (disposable != null) disposable.Dispose();
        }

        public object GetService(Type serviceType)
        {
            return _resolutionRoot.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _resolutionRoot.GetAll(serviceType);
        }
    }
}
