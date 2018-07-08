using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DC.Data.Common.DataManage;
using DC.Data.Request.DataManage;
using DC.IService.DataManage;
using MyFX.Core.BaseModel.Result;

namespace DC.Web.Common
{
    public class TableInfoHelper
    {
        public static string GetColumnDesc(TableInfoDto tableInfo, string colName)
        {
            if (colName.ToLower() == "rownum")
            {
                return "编号";
            }
            return tableInfo.ColumnInfos.Single(c => c.Name.Equals(colName)).Desc;
        }

        public static TableInfoDto GetTableInfo(ITableInfoService tableInfoService,string tabName)
        {
            var tableRs = tableInfoService.GetTable(
                   new GetTableRequest() { TableName = tabName }
                );

            tableRs.CheckErrorAndThrowIt();
            var tabInfo = tableRs.Data;
            if (tabInfo == null)
            {
                throw new MyFX.Core.Exceptions.AppServiceException(string.Format("未查询到表[{0}]的信息", tabName));
            }

            return tabInfo;
        }
    }
}