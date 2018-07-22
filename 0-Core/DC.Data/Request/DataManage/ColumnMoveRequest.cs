using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Common.DataManage;
using MyFX.Core.BaseModel.Request;

namespace DC.Data.Request.DataManage
{
    public class ColumnMoveRequest : RequestBase
    {
        public string TabName { get; set; }
        public string ColName { get; set; }
        public MoveType MoveType { get; set; }
    }
}
