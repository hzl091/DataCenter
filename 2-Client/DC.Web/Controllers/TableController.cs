using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Autofac;
using DC.Common.DataManage;
using DC.Data.Request.DataManage;
using DC.DAL;
using DC.IService.DataManage;
using DC.Service.DataManage;
using DC.Web.Common;
using DC.Web.Models;
using MyFX.Core.Base;
using MyFX.Core.BaseModel.Paging;
using MyFX.Core.BaseModel.Result;
using MyFX.Core.DI;
using MyFX.Core.Domain.Uow;
using MyFX.Core.Logs;
using MyFX.Log.Log4Net;
using WebGrease.Css.Extensions;

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

        #region Index
        public ActionResult Index()
        {
            var res = _tableInfoService.FindTables(new FindTablesRequest());
            res.CheckErrorAndThrowIt();
            return View(res.Data);
        }
        #endregion

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Cols(string tabName)
        {
            var res = _tableInfoService.GetTable(new GetTableRequest() {TableName = tabName});
            res.CheckErrorAndThrowIt();
            return View(res.Data);
        }

        [HttpPost]
        public ActionResult ColMove(string tabName, string colName, MoveType moveType)
        {
            var res = _tableInfoService.ColumnMove(new ColumnMoveRequest()
            {
                TabName = tabName,
                ColName = colName,
                MoveType = moveType
            });
            res.CheckErrorAndThrowIt();

            var tabRes = _tableInfoService.GetTable(new GetTableRequest() { TableName = tabName });
            res.CheckErrorAndThrowIt();
            return PartialView("ColsTable", tabRes.Data);
        }

        #region Detail
        public ActionResult Detail(string tabName, string orderBy = "ID", string sort = "DESC", int pageIndex = 1)
        {
            int pageSize = 20;
            var model = TableDataHelper.GetPagerData(_tableInfoService, _tableDataService, tabName, orderBy, sort, pageSize, pageIndex);
            return View(model);
        }
        #endregion

        #region AddData
        public ActionResult AddData(string tabName)
        {
            var tabInfo = TableInfoHelper.GetTableInfo(_tableInfoService, tabName);
            return View(tabInfo);
        }

        [HttpPost]
        public ActionResult AddData(string tabName, FormCollection form)
        {
            var tabInfo = TableInfoHelper.GetTableInfo(_tableInfoService, tabName);
            var keys = form.AllKeys;
            StringBuilder sb = new StringBuilder();
            foreach (string colName in keys)
            {
                var name = colName;
                var colConfig = tabInfo.ColumnInfos.SingleOrDefault(c => c.Name == name);
                if (colConfig != null)
                {
                    sb.AppendLine(string.Format("declare @{0} {1};", colConfig.Name, colConfig.Type));
                    if (colConfig.FormItemType == FormItemType.Double
                        || colConfig.FormItemType == FormItemType.Money
                        || colConfig.FormItemType == FormItemType.Number)
                    {
                        sb.AppendLine(string.Format("set @{0} = {1};", colConfig.Name, form[colName]));
                    }
                    else
                    {
                        sb.AppendLine(string.Format("set @{0} = '{1}';", colConfig.Name, form[colName]));
                    }  
                }
                else
                {
                    form.Remove(colName);
                }
            }

            string fields = "";
            string values = "";
            form.AllKeys.ForEach(key =>
            {
                fields += string.Format("[{0}],", key);
                values += string.Format("@{0},", key);
            });
            fields = fields.Substring(0, fields.Length - 1);
            values = values.Substring(0, values.Length - 1);

            sb.AppendLine(string.Format("insert into [{0}] ({1}) values({2})", tabName, fields, values));
            string sql = sb.ToString();

            SqlHelper.ExecuteNonQuery(sql);
            return RedirectToAction("AddData", new {tabName = tabName});
        }
        #endregion
    }
}