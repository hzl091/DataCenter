using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFX.Core.BaseModel.Request;

namespace DC.Data.Request.DataManage
{
    public class FindTablesRequest : RequestBase
    {
        /// <summary>
        /// 要查找的表名集合
        /// </summary>
        public string[] TableNames { get; set; }
    }
}
