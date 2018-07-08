using System;
using System.Data;
using System.Linq;
using DC.Common.DataManage;
using DC.Data.Common.DataManage;
using DC.Domain.DataManage;

namespace DC.Web.Common
{
    public class HtmlHelper
    {
        #region ConvertDataTableToHtml
        public static string ConvertDataTableToHtml(TableDataInfo tableDataInfo, string orderBy, string sort)
        {
            DataTable dataTable = tableDataInfo.TableData;
            TableInfoDto tableInfoDto = tableDataInfo.TableInfo;

            string html = "<table class=\"table table-bordered table-hover table-condensed\"><tbody>";
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
            html += "</tbody></table>";
            return html;
        }
        #endregion

        public static string CreateHtmlForm(TableInfoDto tableInfo, string formAction)
        {
            string html = string.Format("<h2>{0}</h2>", tableInfo.Name);
            var cols = tableInfo.ColumnInfos.Where(c => !c.IsPrimaryKey)
                .OrderBy(c => c.Sort);

            html += string.Format("<form action=\"{0}\" method=\"post\">", formAction);
            foreach (var col in cols)
            {
                html += "<ul>";
                html += string.Format("<li>{0}</li>", col.Desc);
                switch (col.FormItemType)
                {
                    case FormItemType.Text:
                        html += string.Format("<input type=\"text\" id=\"{0}\" name=\"{0}\">", col.Name);
                        break;
                    case FormItemType.BigText:
                        html += string.Format("<textarea rows=\"10\" cols=\"80\" id=\"{0}\" name=\"{0}\">", col.Name);
                        break;
                    case FormItemType.Double:
                        html += string.Format("<input type=\"text\" id=\"{0}\" name=\"{0}\">", col.Name);
                        break;
                    case FormItemType.Number:
                        html += string.Format("<input type=\"text\" id=\"{0}\" name=\"{0}\">", col.Name);
                        break;
                    case FormItemType.DateTime:
                        html += string.Format("<input type=\"text\" id=\"{0}\" name=\"{0}\">", col.Name);
                        break;
                    case FormItemType.Money:
                        html += string.Format("<input type=\"text\" id=\"{0}\" name=\"{0}\">", col.Name);
                        break;
                }
                html += "<li>";
                html += "</ul>";
            }

            html += "<input type=\"submit\" value=\"确定\">";
            html += "</form>";
            return html;
        }
    }
}
