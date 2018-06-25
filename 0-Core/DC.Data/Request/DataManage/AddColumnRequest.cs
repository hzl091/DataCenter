using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Data.Common.DataManage;
using MyFX.Core.BaseModel.Request;

namespace DC.Data.Request.DataManage
{
    public class AddColumnRequest : RequestBase
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 列信息
        /// </summary>
        public IEnumerable<ColumnInfoDto> ColumnInfos { get; set; }
    }
}
