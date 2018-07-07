/****************************************************************************************
 * 文件名：GetPagerDataRequest
 * 作者：huangzl
 * 创始时间：2018/6/27 18:10:05
 * 创建说明：
****************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFX.Core.BaseModel.Request;

namespace DC.Data.Request.DataManage
{
    public class GetPagerDataRequest : PageRequestBase
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 条件
        /// </summary>
        public string Where { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public string OrderBy { get; set; }
    }
}
