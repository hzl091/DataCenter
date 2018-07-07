using System.Data;
using DC.Web.Common;

namespace DC.Web.Models
{
    public class TableModel
    {
        public Webdiyer.WebControls.Mvc.PagedList<DataRow> PagedList { get; set; }

        public TableDataInfo TableInfo { get; set; }

        public RequestInfo RequestInfo { get; set; }
    }

    public class RequestInfo
    {
        public int PageIndex { get; set; }

        public string OrderBy { get; set; }

        public string Sort { get; set; }
    }
}