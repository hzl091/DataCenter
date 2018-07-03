using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac;
using DC.Data.Request.DataManage;
using DC.DAL;
using DC.IService.DataManage;
using DC.Service.DataManage;
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

        public TableController(ITableInfoService tableInfoService)
        {
            _tableInfoService = tableInfoService;
        }

        // GET: Table
        public ActionResult Index()
        {
            var res = _tableInfoService.FindTables(new FindTablesRequest());
            res.CheckErrorAndThrowIt();
            return View(res.Data);
        }

        public ActionResult Detail(int id, string orderBy = "ID", string sort = "DESC", int pageIndex = 1)
        {
            return View();
        }
    }
}