using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFX.Core.Domain.Entities;
using DC.Common.DataManage;

namespace DC.Domain.DataManage
{
    /// <summary>
    /// 列信息
    /// </summary>
    public class ColumnInfo : EntityBase<int>
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型,如nvachar(20)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        ///是否为主键列 
        /// </summary>
        public bool IsPrimaryKey { get; set; }
        
        /// <summary>
        /// 是否为系统固定列(不可删除)
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 表单项类型
        /// </summary>
        public FormItemType FormItemType { get; set; }

        /// <summary>
        /// 表ID
        /// </summary>
        public int TableInfoId { get; set; }

        /// <summary>
        /// 表信息导航属性
        /// </summary>
        public TableInfo TableInfo { get; set; }
    }
}
