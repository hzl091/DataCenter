/****************************************************************************************
 * 文件名：ITableDataService
 * 作者：huangzl
 * 创始时间：2018/6/27 18:02:53
 * 创建说明：
****************************************************************************************/

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Common;
using DC.Data.Request.DataManage;
using MyFX.Core.BaseModel.Request;
using MyFX.Core.DI;

namespace DC.IService.DataManage
{
    public interface ITableDataService : IDependency
    {
        /// <summary>
        /// 获取表信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        MyFX.Core.BaseModel.Result.ResultObject<PagerInfo> GetPagerData(GetPagerDataRequest request);
    }
}
