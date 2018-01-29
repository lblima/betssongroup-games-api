[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(OnlineCassino.WebApi.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(OnlineCassino.WebApi.App_Start.NinjectWebCommon), "Stop")]

namespace OnlineCassino.WebApi.App_Start
{
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using OnlineCassino.Domain.Interfaces;
    using OnlineCassino.Infrastructure;
    using OnlineCassino.WebApi.Providers;
    using System;
    using System.Web;

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
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //I could use (bind) the repositories interfaces itself instead IUnitOfWork, but as I have only few repos, I´ve unified all repos interface in just one (IUnitOfWork)

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();

            //I used this interface in order to access "System.Web.HttpContext.Current.User.Identity.GetUserId()" from controller tests
            kernel.Bind<IIdentityProvider>().To<IdentityProvider>();
        }
    }
}