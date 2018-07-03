using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using DC.DAL;
using DC.DAL.IRepository;
using DC.DAL.Repository;
using DC.IService.DataManage;
using DC.Service.DataManage;
using MyFX.Core.DI;
using MyFX.Core.Domain.Uow;
using MyFX.Core.Logs;
using MyFX.Log.Log4Net;

namespace WebApplication1
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //Action<ContainerBuilder> act = builder =>
            //{
            //    builder.RegisterType<DCUnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope(); //配置使用的工作单元
            //    builder.RegisterType<LogFactory>().As<ILogFactory>(); //配置使用的日志工厂

            //    builder.RegisterType<ITableInfoService>().As<TableInfoService>().InstancePerLifetimeScope();
            //    builder.RegisterType<ITableDataService>().As<TableDataService>().InstancePerLifetimeScope();
            //    builder.RegisterType<ITableInfoRepository>().As<TableInfoRepository>().InstancePerLifetimeScope();

            //};

            ////var container = DIBootstrapper.Initialize(act, "DC.DAL", "DC.Service");
            //var container = DIBootstrapper.Initialize(act,new string[]{});
            //var resolver = new AutofacWebApiDependencyResolver(container);
            //GlobalConfiguration.Configuration.DependencyResolver = resolver;

            var builder = new ContainerBuilder();

            //builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<DCUnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope(); //配置使用的工作单元
            builder.RegisterType<LogFactory>().As<ILogFactory>(); //配置使用的日志工厂
            builder.RegisterType<TableInfoService>().As<ITableInfoService>().InstancePerRequest();
            builder.RegisterType<TableDataService>().As<ITableDataService>().InstancePerLifetimeScope();
            builder.RegisterType<TableInfoRepository>().As<ITableInfoRepository>().InstancePerRequest();
 
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}
