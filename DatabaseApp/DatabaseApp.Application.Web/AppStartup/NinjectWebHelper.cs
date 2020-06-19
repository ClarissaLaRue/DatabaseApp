using System.Web;
using System;
using Database.Application.Web.AppStartup;
using Database.Common.Helper;
using Database.DataAccess.Entities;
using Database.DataAccess.Entities.Base;
using Database.DataAccess.Entities.Building;
using Database.DataAccess.Repositories.Base;
using Database.DataAccess.Repositories.Base.Interfaces;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebHelper), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebHelper), "Stop")]

namespace Database.Application.Web.AppStartup
{
    public class NinjectWebHelper
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);

                RegisterServices(kernel);
            }
            catch
            {
                kernel.Dispose();
                throw;
            }

            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
            NinjectHelper.Kernel = kernel;
            
            NinjectHelper.Kernel.Bind<DbContextBase>().To<ApplicationContext>().InRequestScope();
            
            NinjectHelper.Kernel.Bind<ISqlRepository<SportBuilding>>().To<SqlRepositoryBase<SportBuilding>>().InRequestScope();
        }
    }
}