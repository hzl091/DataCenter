using System.Collections.Generic;
using System.Linq;
using System.Text;
using DC.Data.Common.DataManage;
using DC.Data.Request.DataManage;
using DC.DAL;
using DC.DAL.IRepository;
using DC.Domain.DataManage;
using DC.Service.Validators;
using MyFX.Core.Actions;
using MyFX.Core.BaseModel.Result;
using MyFX.Core.Domain.Uow;

namespace DC.Service.Core
{
    public class AddColumnCore : TransactionServiceOptionBase<AddColumnRequest, ResultObject>
    {
        private readonly ITableInfoRepository _tableInfoRepository = null;
        private readonly IUnitOfWork _uow = null;

        public AddColumnCore(AddColumnRequest request, IUnitOfWork uow, ITableInfoRepository tableInfoRepository)
            : base(request)
        {
            _uow = uow;
            _tableInfoRepository = tableInfoRepository;
            SetValidator(new AddColumnValidator());
        }

        protected override ResultObject Execute()
        {
            var tabInfo = _tableInfoRepository.First(t => t.Name == Request.Name);
            if (tabInfo == null)
            {
                throw new MyFX.Core.Exceptions.AppServiceException(string.Format("名为[{0}]的表不存在", Request.Name));
            }

            foreach (var item in Request.ColumnInfos)
            {
                tabInfo.AddColumnInfo(item.Name, item.FormItemType, item.Desc, item.IsPrimaryKey, item.IsSystem, item.Sort);
            }
            _uow.Commit();

            string addColumnSql = GetAddColumnSql(tabInfo);
            SqlHelper.ExecuteNonQuery(addColumnSql);
            return new BatchResultObject();
        }

        private string GetAddColumnSql(TableInfo tableInfo)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var columnInfoDto in Request.ColumnInfos)
            {
                var columnInfo = tableInfo.ColumnInfos.First(c => c.Name == columnInfoDto.Name);
                sb.AppendLine(string.Format("alter table [{0}] add [{1}] {2};", Request.Name, columnInfo.Name, columnInfo.Type));
            }

            return sb.ToString();
        }
    }
}
