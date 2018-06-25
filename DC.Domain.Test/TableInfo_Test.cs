using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DC.Domain.DataManage;

namespace DC.Domain.Test
{
    [TestClass]
    public class TableInfo_Test
    {
        [TestMethod]
        public void CreateTable_Test()
        {
            var tabInfo = new TableInfo("customer", "客户表");
            tabInfo.AddColumnInfo("id", Common.DataManage.FormItemType.Number, "id",true, true);

            tabInfo.AddColumnInfo("code", Common.DataManage.FormItemType.Text, "客户编号", false,false);
            tabInfo.AddColumnInfo("name", Common.DataManage.FormItemType.Text, "客户名称", false, false);
            tabInfo.AddColumnInfo("company", Common.DataManage.FormItemType.Text, "客户公司", false, false);
            tabInfo.AddColumnInfo("sex", Common.DataManage.FormItemType.RadioButtonList, "客户性别", false, false);

            string sql = tabInfo.GetColumnSql();
            Console.WriteLine(sql);
        }
    }
}
