using System.Collections.Generic;
using DC.Data.Common.DataManage;
using MyFX.Core.BaseModel.Request;

namespace DC.Data.Request.DataManage
{
    public class CreateTableRequest : RequestBase
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 列信息
        /// </summary>
        public IEnumerable<ColumnInfoDto> ColumnInfos { get; set; }
    }
}
