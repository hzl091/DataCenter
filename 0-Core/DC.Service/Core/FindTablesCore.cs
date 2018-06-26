using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using DC.Data.Common.DataManage;
using DC.Data.Request.DataManage;
using DC.DAL.IRepository;
using DC.Domain.DataManage;
using MyFX.Core;
using MyFX.Core.Actions;
using MyFX.Core.BaseModel.Result;

namespace DC.Service.Core
{
    public class FindTablesCore : TransactionServiceOptionBase<FindTablesRequest, ResultObject<IEnumerable<TableInfoDto>>>
    {
        private readonly ITableInfoRepository _tableInfoRepository = null;

        public FindTablesCore(FindTablesRequest request, ITableInfoRepository tableInfoRepository) 
            : base(request)
        {
            _tableInfoRepository = tableInfoRepository;
        }

        protected override ResultObject<IEnumerable<TableInfoDto>> Execute()
        {
            var tableNames = Request.TableNames;
            List<TableInfo> tableInfos;

            if (tableNames != null && tableNames.Any())
            {
                tableInfos = _tableInfoRepository.Find(t => tableNames.Contains(t.Name)).ToList();
            }
            else
            {
                tableInfos = _tableInfoRepository.FindAll().ToList();
            }

            return new ResultObject<IEnumerable<TableInfoDto>>(tableInfos.MapToList<TableInfo, TableInfoDto>());
        }
    }
}
