using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Common.DataManage;

namespace DC.Data.Common.DataManage
{
    public class ColumnInfoDto
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 表单项类型
        /// </summary>
        public FormItemType FormItemType { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 是否为主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 是否为系统列
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int Sort { get; set; }
    }
}
