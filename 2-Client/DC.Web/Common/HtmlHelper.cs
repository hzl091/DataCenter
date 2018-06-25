using System;
using System.Data;
using DC.Data.Common.DataManage;
using DC.Domain.DataManage;

namespace DC.Web.Common
{
    public class HtmlHelper
    {
        public static string ConvertDataTableToHtml(TableDataInfo tableDataInfo, string orderBy, string sort)
        {
            DataTable dataTable = tableDataInfo.TableData;
            TableInfoDto tableInfoDto = tableDataInfo.TableInfo;

            string html = "<table border=\"1\">";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dataTable.Columns.Count; i++)
            { 
                string colName = dataTable.Columns[i].ColumnName;
                if (orderBy.ToLower().Equals(colName.ToLower()))
                {
                    if (sort.ToLower().Equals("asc"))
                    {
                        html += string.Format("<td><a href=\"?orderby={1}&sort={2}\">{0}</a></td>", TableInfoHelper.GetColumnDesc(tableInfoDto, colName), colName, "DESC");
                    }
                    else
                    {
                        html += string.Format("<td><a href=\"?orderby={1}&sort={2}\">{0}</a></td>", TableInfoHelper.GetColumnDesc(tableInfoDto, colName), colName, "ASC");
                    }
                }
                else
                {
                    html += string.Format("<td><a href=\"?orderby={1}&sort={2}\">{0}</a></td>", TableInfoHelper.GetColumnDesc(tableInfoDto, colName), colName, "DESC");
                }
            }
            html += "</tr>";
            //add rows
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dataTable.Columns.Count; j++)
                    html += "<td>" + dataTable.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

    }
}
