/****************************************************************************************
 * 文件名：SaveTableRequest
 * 作者：huangzl
 * 创始时间：2018/6/26 11:17:25
 * 创建说明：
****************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Common;
using DC.Data.Common.DataManage;
using MyFX.Core.BaseModel.Request;

namespace DC.Data.Request.DataManage
{
    public class SaveTableRequest : RequestBase
    {
        /// <summary>
        /// 操作类型
        /// </summary>
        public OperationType OperationType { get; set; }

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
