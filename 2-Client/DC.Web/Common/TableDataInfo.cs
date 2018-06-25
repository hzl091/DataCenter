using System.Collections.Generic;
using System.Data;
using System.Linq;
using DC.Data.Common.DataManage;

namespace DC.Web.Common
{
    public class TableDataInfo
    {
        public TableInfoDto TableInfo { get; set; }

        public DataTable TableData { get; set; }
    }

    public class DataRowDataInfo
    {
        public TableInfoDto TableInfo { get; set; }

        public IEnumerable<DataRow> Rows { get; set; }
    }

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
    }
}
