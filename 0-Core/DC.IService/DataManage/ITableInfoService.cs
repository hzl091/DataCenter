using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Data.Request.DataManage;
using MyFX.Core.BaseModel;
using MyFX.Core.DI;

namespace DC.IService.DataManage
{
    public interface ITableInfoService : IDependency
    {
        MyFX.Core.BaseModel.Result.ResultObject CreateTable(CreateTableRequest request);
    }
}
