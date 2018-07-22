using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Data.Common.DataManage;
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

        /// <summary>
        /// 创建或更新表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MyFX.Core.BaseModel.Result.ResultObject SaveTable(SaveTableRequest request);

        /// <summary>
        /// 列排序调整
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MyFX.Core.BaseModel.Result.ResultObject ColumnMove(ColumnMoveRequest request);

        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MyFX.Core.BaseModel.Result.ResultObject<TableInfoDto> GetTable(GetTableRequest request);

        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MyFX.Core.BaseModel.Result.ResultObject<IEnumerable<TableInfoDto>> FindTables(FindTablesRequest request);
    }
}
