using System;
using Autofac;
using DC.Data.Request.DataManage;
using DC.DAL;
using DC.IService.DataManage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFX.Core.BaseModel.Result;
using MyFX.Core.DI;
using MyFX.Core.Domain.Uow;
using MyFX.Core.Logs;
using MyFX.Log.Log4Net;

namespace DC.Service.Test
{
    [TestClass]
    public class TableDataServiceTest
    {
        private IContainer GetContainer()
        {
            Action<ContainerBuilder> act = builder =>
            {
                builder.RegisterType<DCUnitOfWork>().As<IUnitOfWork>(); //配置使用的工作单元
                builder.RegisterType<LogFactory>().As<ILogFactory>(); //配置使用的日志工厂
            };

            var container = DIBootstrapper.Initialize(act, new string[] { "DC.DAL", "DC.Service" });
            return container;
        }

        [TestMethod]
        public void GetPagerData_Test()
        {
            var ci = GetContainer();
            var tableDataService = ci.Resolve<ITableDataService>();

            var res = tableDataService.GetPagerData(new GetPagerDataRequest()
            {
                TableName = "TestTabe",
               PageIndex = 1,
               PageSize = 10
            });
            res.CheckErrorAndThrowIt();
        }
    }
}
