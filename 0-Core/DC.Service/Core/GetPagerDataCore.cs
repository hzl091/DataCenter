/****************************************************************************************
 * 文件名：GetPagerDataCore
 * 作者：huangzl
 * 创始时间：2018/6/27 18:25:27
 * 创建说明：
****************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Common;
using DC.Data.Common.DataManage;
using DC.Data.Request.DataManage;
using DC.DAL;
using DC.DAL.IRepository;
using DC.Domain.DataManage;
using DC.Service.Validators;
using MyFX.Core;
using MyFX.Core.Actions;
using MyFX.Core.BaseModel.Result;

namespace DC.Service.Core
{
    public class GetPagerDataCore : ServiceOptionBase<GetPagerDataRequest, ResultObject<PagerInfo>>
    {
        private readonly ITableInfoRepository _tableInfoRepository = null;

        public GetPagerDataCore(GetPagerDataRequest request, ITableInfoRepository tableInfoRepository) 
            : base(request)
        {
            _tableInfoRepository = tableInfoRepository;
            SetValidator(new GetPagerDataValidator());
        }

        protected override ResultObject<PagerInfo> Execute()
        {
            int pageSize = Request.PageSize;
            int pageIndex = Request.PageIndex;
            string where = string.IsNullOrEmpty(Request.Where) ? "1=1" : Request.Where;
            string orderBy = string.IsNullOrEmpty(Request.OrderBy) ? "ID DESC" : Request.OrderBy;

            var tableInfo = _tableInfoRepository.Single(t =>t.Name == Request.TableName);
            var pagerData = SqlHelper.GetPagerData(tableInfo.Name, tableInfo.GetColumnSql(), where, orderBy, pageIndex, pageSize);
            return new ResultObject<PagerInfo>(pagerData);
        }
    }
}
