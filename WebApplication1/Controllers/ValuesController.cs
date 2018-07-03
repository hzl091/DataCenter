using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DC.Data.Request.DataManage;
using DC.IService.DataManage;

namespace WebApplication1.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly ITableInfoService _tableInfoService;
        public ValuesController(ITableInfoService tableInfoService)
        {
            _tableInfoService = tableInfoService;
        }

        public string GetTable()
        {
            var x = _tableInfoService.FindTables(new FindTablesRequest());
            return x.Data.First().Name;
        }
    }
}
