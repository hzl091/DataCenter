using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using DC.Common;
using DC.Common.DataManage;
using DC.Data.Common.DataManage;
using DC.Data.Request.DataManage;
using DC.DAL;
using DC.Domain.DataManage;
using DC.IService.DataManage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyFX.Core.BaseModel.Request;
using MyFX.Core.BaseModel.Result;
using MyFX.Core.DI;
using MyFX.Core.Domain.Uow;
using MyFX.Core.Logs;
using MyFX.Log.Log4Net;

namespace DC.Service.Test
{
    [TestClass]
    public class TableInfoServiceTest
    {
        private IContainer GetContainer()
        {
            Action<ContainerBuilder> act = builder =>
            {
                builder.RegisterType<DCUnitOfWork>().As<IUnitOfWork>(); //配置使用的工作单元
                builder.RegisterType<LogFactory>().As<ILogFactory>(); //配置使用的日志工厂
            };

            Service.MapperCfg.Initialize();
            var container = DIBootstrapper.Initialize(act, new string[] { "DC.DAL", "DC.Service" });
            return container;
        }

        [TestMethod]
        public void CreateTable_Test()
        {
            var ci = GetContainer();
            var tableInfoService = ci.Resolve<ITableInfoService>();
            var request = new CreateTableRequest();
            request.Name = "Order";
            request.Desc = "订单表";
            List<ColumnInfoDto> cols = new List<ColumnInfoDto>();
            cols.Add(new ColumnInfoDto()
            {
                Name = "Id",
                Desc = "Id",
                FormItemType = FormItemType.Number,
                IsPrimaryKey = true,
                IsSystem = true
            });

            cols.Add(new ColumnInfoDto()
            {
                Name = "OrderNo",
                Desc = "订单号",
                FormItemType = FormItemType.Text,
                IsPrimaryKey = false,
                IsSystem = false
            });

            cols.Add(new ColumnInfoDto()
            {
                Name = "Total",
                Desc = "总价值",
                FormItemType = FormItemType.Money,
                IsPrimaryKey = false,
                IsSystem = false
            });

            request.ColumnInfos = cols;

            var res = tableInfoService.CreateTable(request);
            res.CheckErrorAndThrowIt();
        }

        [TestMethod]
        public void AddColumn_Test()
        {
            var ci = GetContainer();
            var tableInfoService = ci.Resolve<ITableInfoService>();
            var request = new AddColumnRequest();

            request.Name = "Order";
            List<ColumnInfoDto> cols = new List<ColumnInfoDto>();
            cols.Add(new ColumnInfoDto()
            {
                Name = "FullName",
                Desc = "收货人",
                FormItemType = FormItemType.Text,
                IsPrimaryKey = false,
                IsSystem = false
            });

            cols.Add(new ColumnInfoDto()
            {
                Name = "Tel",
                Desc = "电话",
                FormItemType = FormItemType.Text,
                IsPrimaryKey = false,
                IsSystem = false
            });

            request.ColumnInfos = cols;

            var res = tableInfoService.AddColumn(request);
            res.CheckErrorAndThrowIt();
        }

        [TestMethod]
        public void SaveTable_Test()
        {
            var ci = GetContainer();
            var tableInfoService = ci.Resolve<ITableInfoService>();
            var request = new SaveTableRequest();
            request.Name = "Order";
            request.Desc = "订单表";
            request.OperationType = OperationType.Edit;

            List<ColumnInfoDto> cols = new List<ColumnInfoDto>();
            cols.Add(new ColumnInfoDto()
            {
                Name = "FullName",
                Desc = "收货人",
                FormItemType = FormItemType.Text,
                IsPrimaryKey = false,
                IsSystem = true,
                Sort = 7
            });

            request.ColumnInfos = cols;
            var res = tableInfoService.SaveTable(request);
            res.CheckErrorAndThrowIt();
        }

        [TestMethod]
        public void GetTable_Test()
        {
            var ci = GetContainer();
            var tableInfoService = ci.Resolve<ITableInfoService>();
            var request = new GetTableRequest();
            request.TableName = "Order";

            var res = tableInfoService.GetTable(request);
            res.CheckErrorAndThrowIt();
        }

        [TestMethod]
        public void FindTables_Test()
        {
            var ci = GetContainer();
            var tableInfoService = ci.Resolve<ITableInfoService>();
            var request = new FindTablesRequest();
            request.TableNames = new string[] { "Order" };

            var res = tableInfoService.FindTables(request);
            res.CheckErrorAndThrowIt();
        }
        
    }
}
