using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.Data.Common.DataManage
{
    public class TableInfoDto
    {
        /// <summary>
        /// Id编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 列集合信息
        /// </summary>
        public ICollection<ColumnInfoDto> ColumnInfos { get; set; }
    }
}
