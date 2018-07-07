using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Data.Common.DataManage;
using DC.Data.Request.DataManage;
using DC.DAL.IRepository;
using DC.IService.DataManage;
using DC.Service.Core;
using MyFX.Core.BaseModel.Result;
using MyFX.Core.Domain.Uow;

namespace DC.Service.DataManage
{
    public class TableInfoService : ITableInfoService
    {
        private readonly ITableInfoRepository _tableInfoRepository = null;
        private readonly IUnitOfWork _uow = null;

        public TableInfoService(ITableInfoRepository tableInfoRepository, IUnitOfWork uow)
        {
            _tableInfoRepository = tableInfoRepository;
            _uow = uow;
        }
        public ResultObject CreateTable(CreateTableRequest request)
        {
            var core = new CreateTableCore(request, _uow, _tableInfoRepository);
            return core.DoExecute();
        }

        public ResultObject AddColumn(AddColumnRequest request)
        {
            var core = new AddColumnCore(request, _uow, _tableInfoRepository);
            return core.DoExecute();
        }

        public ResultObject SaveTable(SaveTableRequest request)
        {
            var core = new SaveTableCore(request, _uow, _tableInfoRepository);
            return core.DoExecute();
        }

        public ResultObject<TableInfoDto> GetTable(GetTableRequest request)
        {
            var core = new GetTableCore(request, _tableInfoRepository);
            return core.DoExecute();
        }

        public ResultObject<IEnumerable<TableInfoDto>> FindTables(FindTablesRequest request)
        {
            var core = new FindTablesCore(request, _tableInfoRepository);
            return core.DoExecute();
        }
    }
}
