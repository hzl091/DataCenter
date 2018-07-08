using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DC.Data.Request.DataManage;
using DC.IService.DataManage;
using DC.Web.Models;
using MyFX.Core.BaseModel.Result;

namespace DC.Web.Common
{
    public class TableDataHelper
    {
        public static TableModel GetPagerData(ITableInfoService tableInfoService, ITableDataService tableDataService, string tabName, string orderBy, string sort, int pageSize, int pageIndex = 1)
        {
            var tabInfo = TableInfoHelper.GetTableInfo(tableInfoService, tabName);

            pageSize = pageSize <= 0 ? 20 : pageSize;
            string orderInfo = "";
            if (string.IsNullOrEmpty(orderBy) || string.IsNullOrEmpty(sort))
            {
                orderInfo = "[ID] DESC";
            }
            else
            {
                orderInfo = string.Format("[{0}] {1}", orderBy, sort);
            }

            var tableDataRs = tableDataService.GetPagerData(new GetPagerDataRequest()
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

            var requestInfo = new Models.RequestInfo
            {
                OrderBy = orderBy,
                Sort = sort,
                PageIndex = pageIndex
            };

            model.PagedList = arts;
            model.TableInfo = info;
            model.RequestInfo = requestInfo;

            return model;
        }
    }
}