using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using MyFX.Core.Base;
using MyFX.Core.BaseModel;
using MyFX.Core.BaseModel.Paging;
using MyFX.Core.BaseModel.Request;
using MyFX.Core.DI;
using MyFX.Core.Domain.Uow;
using MyFX.Core.Events;
using MyFX.Core.Logs;
using MyFX.Log.Log4Net;
using MyFX.Repository.Ef;
using DC.DAL;
using DC.DAL.IRepository;
using DC.Domain.DataManage;

namespace DC.Repository.Test
{
    [TestClass]
    public class TableInfoRepositoryTest
    {
        private IContainer GetContainer()
        {
            Action<ContainerBuilder> act = builder =>
            {
                builder.RegisterType<DCUnitOfWork>().As<IUnitOfWork>(); //配置使用的工作单元
                builder.RegisterType<LogFactory>().As<ILogFactory>(); //配置使用的日志工厂
            };

            var container = DIBootstrapper.Initialize(act, new string[] { "DC.DAL" });
            return container;
        }

        [TestMethod]
        public void SaveTableInfo_Test()
        {
            var ci = GetContainer();
            var uow = ci.Resolve<IUnitOfWork>();
            ITableInfoRepository rep = ci.Resolve<ITableInfoRepository>();

            var tabInfo = new TableInfo("customer", "客户表");
            tabInfo.AddColumnInfo("id", Common.DataManage.FormItemType.Number, "id", true, true);

            tabInfo.AddColumnInfo("code", Common.DataManage.FormItemType.Text, "客户编号", false, false);
            tabInfo.AddColumnInfo("name", Common.DataManage.FormItemType.Text, "客户名称", false, false);
            tabInfo.AddColumnInfo("company", Common.DataManage.FormItemType.Text, "客户公司", false, false);
            tabInfo.AddColumnInfo("sex", Common.DataManage.FormItemType.RadioButtonList, "客户性别", false, false);

            rep.Add(tabInfo);
            uow.Commit();
        }

        [TestMethod]
        public void GetTableInfo_Test()
        {
            var ci = GetContainer();
            ITableInfoRepository rep = ci.Resolve<ITableInfoRepository>();
            var tableInfo = rep.GetByKey(1);
            var x = tableInfo.ColumnInfos;
            Assert.IsNotNull(tableInfo);
        }
    }
}
