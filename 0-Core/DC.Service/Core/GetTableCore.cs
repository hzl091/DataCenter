using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using DC.Data.Common.DataManage;
using DC.Data.Request.DataManage;
using DC.DAL.IRepository;
using DC.Domain.DataManage;
using DC.Service.Validators;
using MyFX.Core;
using MyFX.Core.Actions;
using MyFX.Core.BaseModel.Result;

namespace DC.Service.Core
{
    public class GetTableCore : ServiceOptionBase<GetTableRequest, ResultObject<TableInfoDto>>
    {
        private readonly ITableInfoRepository _tableInfoRepository = null;

        public GetTableCore(GetTableRequest request, ITableInfoRepository tableInfoRepository) 
            : base(request)
        {
            _tableInfoRepository = tableInfoRepository;
            SetValidator(new GetTableValidator());
        }

        protected override ResultObject<TableInfoDto> Execute()
        {
           TableInfo tableInfo = _tableInfoRepository.Find(t => t.Name == Request.TableName).SingleOrDefault();
           return new ResultObject<TableInfoDto>(AutoMapper.Mapper.Instance.Map<TableInfo, TableInfoDto>(tableInfo));
        }
    }
}
