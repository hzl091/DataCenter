using System;
using System.Data;
using System.Linq;
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
                if (tableInfoDto.ColumnInfos.All(c => c.Name != colName))
                {
                    continue;
                }

                if (orderBy.ToLower().Equals(colName.ToLower()))
                {
                    if (sort.ToLower().Equals("asc"))
                    {
                        html += string.Format("<td><a href=\"?tabName={1}&orderby={2}&sort={3}\">{0}</a></td>", TableInfoHelper.GetColumnDesc(tableInfoDto, colName), tableInfoDto.Name, colName, "DESC");
                    }
                    else
                    {
                        html += string.Format("<td><a href=\"?tabName={1}&orderby={2}&sort={3}\">{0}</a></td>", TableInfoHelper.GetColumnDesc(tableInfoDto, colName), tableInfoDto.Name, colName, "ASC");
                    }
                }
                else
                {
                    html += string.Format("<td><a href=\"?tabName={1}&orderby={2}&sort={3}\">{0}</a></td>", TableInfoHelper.GetColumnDesc(tableInfoDto, colName), tableInfoDto.Name, colName, "DESC");
                }
            }
            html += "</tr>";
            //add rows
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    string colName = dataTable.Columns[j].ColumnName;
                    if (tableInfoDto.ColumnInfos.All(c => c.Name != colName))
                    {
                        continue;
                    }
                    html += "<td>" + dataTable.Rows[i][j].ToString() + "</td>";
                }
                
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

    }
}
