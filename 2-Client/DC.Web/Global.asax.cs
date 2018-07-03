using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using DC.Data.Common.DataManage;
using DC.DAL;
using DC.DAL.IRepository;
using DC.DAL.Repository;
using DC.Domain.DataManage;
using DC.IService.DataManage;
using DC.Service.DataManage;
using MyFX.Core.DI;
using MyFX.Core.Domain.Uow;
using MyFX.Core.Logs;
using MyFX.Log.Log4Net;

namespace DC.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Action<ContainerBuilder> act = builder =>
            {
                builder.RegisterType<DCUnitOfWork>().As<IUnitOfWork>(); //配置使用的工作单元
                builder.RegisterType<LogFactory>().As<ILogFactory>(); //配置使用的日志工厂
                builder.RegisterControllers(typeof(MvcApplication).Assembly);
            };

            var container = DIBootstrapper.Initialize(act, "DC.DAL", "DC.Service");
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            DC.Service.MapperCfg.Initialize();
        }
    }
}
