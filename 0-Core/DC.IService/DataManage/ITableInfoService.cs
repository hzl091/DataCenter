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
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MyFX.Core.BaseModel.Result.ResultObject CreateTable(CreateTableRequest request);

        /// <summary>
        /// 添加列
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MyFX.Core.BaseModel.Result.ResultObject AddColumn(AddColumnRequest request);
    }
}
