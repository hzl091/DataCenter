using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using DC.Data.Request.DataManage;
using DC.DAL;
using DC.IService.DataManage;
using DC.Service.DataManage;
using DC.Web.Common;
using DC.Web.Models;
using MyFX.Core.BaseModel.Paging;
using MyFX.Core.BaseModel.Result;
using MyFX.Core.DI;
using MyFX.Core.Domain.Uow;
using MyFX.Core.Logs;
using MyFX.Log.Log4Net;

namespace DC.Web.Controllers
{
    public class TableController : Controller
    {
        private readonly ITableInfoService _tableInfoService;
        private readonly ITableDataService _tableDataService;

        public TableController(ITableInfoService tableInfoService, ITableDataService tableDataService)
        {
            _tableInfoService = tableInfoService;
            _tableDataService = tableDataService;
        }

        // GET: Table
        public ActionResult Index()
        {
            var res = _tableInfoService.FindTables(new FindTablesRequest());
            res.CheckErrorAndThrowIt();
            return View(res.Data);
        }

        public ActionResult Detail(string tabName, string orderBy = "ID", string sort = "DESC", int pageIndex = 1)
        {
            int pageSize = 20;
            var tableRs = _tableInfoService.GetTable(
                   new GetTableRequest(){ TableName = tabName}
                );

            tableRs.CheckErrorAndThrowIt();
            var tabInfo = tableRs.Data;
            if (tabInfo == null)
            {
                throw new MyFX.Core.Exceptions.AppServiceException(string.Format("未查询到表[{0}]的信息", tabName));
            }

            string orderInfo = "";
            if (string.IsNullOrEmpty(orderBy) || string.IsNullOrEmpty(sort))
            {
                orderInfo = "[ID] DESC";
            }
            else
            {
                orderInfo = string.Format("[{0}] {1}", orderBy, sort);
            }


            var tableDataRs = _tableDataService.GetPagerData(new GetPagerDataRequest()
            {
               TableName = tabInfo.Name,
               OrderBy = orderInfo,
               PageSize = pageSize,
               PageIndex = pageIndex
            });
            tableDataRs.CheckErrorAndThrowIt();

            var pagerInfo = tableDataRs.Data;
            TableDataInfo info = new TableDataInfo
            {
                TableData = pagerInfo.PagerData,
                TableInfo = tabInfo
            };

            Webdiyer.WebControls.Mvc.PagedList<DataRow> arts
                = new Webdiyer.WebControls.Mvc.PagedList<DataRow>(pagerInfo.PagerData.Select(), pageIndex, pageSize, pagerInfo.RecordCount);
            TableModel model = new TableModel();

            var requestInfo = new Models.RequestInfo();
            requestInfo.OrderBy = orderBy;
            requestInfo.Sort = sort;
            requestInfo.PageIndex = pageIndex;

            model.PagedList = arts;
            model.TableInfo = info;
            model.RequestInfo = requestInfo;

            return View(model);
        }
    }
}