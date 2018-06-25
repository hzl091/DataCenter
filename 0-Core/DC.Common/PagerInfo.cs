using System.Data;

namespace DC.Common
{
    public class PagerInfo
    {
        /// <summary>
        /// 分页数据
        /// </summary>
        public DataTable PagerData { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int RecordCount { get; set; }
    }
}
