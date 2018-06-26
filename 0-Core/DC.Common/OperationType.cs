using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.Common
{
    /// <summary>
    /// 常用操作类型
    /// </summary>
    public enum OperationType : byte
    {
        /// <summary>
        /// 添加
        /// </summary>
        Add = 10,

        /// <summary>
        /// 编辑
        /// </summary>
        Edit = 20,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 30,

        /// <summary>
        /// 查询
        /// </summary>
        Query = 40
    }
}
