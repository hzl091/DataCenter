using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Common;
using DC.Data.Common.DataManage;
using DC.Data.Request.DataManage;
using DC.DAL.IRepository;
using DC.IService.DataManage;
using DC.Service.Core;
using MyFX.Core.BaseModel.Request;
using MyFX.Core.BaseModel.Result;
using MyFX.Core.Domain.Uow;

namespace DC.Service.DataManage
{
    public class TableDataService : ITableDataService
    {
        private readonly ITableInfoRepository _tableInfoRepository = null;
        private readonly IUnitOfWork _uow = null;

        public TableDataService(ITableInfoRepository tableInfoRepository, IUnitOfWork uow)
        {
            _tableInfoRepository = tableInfoRepository;
            _uow = uow;
        }

        public ResultObject<PagerInfo> GetPagerData(GetPagerDataRequest request)
        {
            var core = new GetPagerDataCore(request, _tableInfoRepository);
            return core.DoExecute();
        }
    }
}
