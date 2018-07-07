using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFX.Core.BaseModel.Request;

namespace DC.Data.Request.DataManage
{
    public class GetTableRequest : RequestBase
    {
        /// <summary>
        /// 要查找的表名
        /// </summary>
        public string TableName { get; set; }
    }
}
