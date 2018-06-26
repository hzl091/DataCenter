using System.Collections.Generic;
using System.Linq;
using System.Text;
using DC.Common;
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
    public class SaveTableCore : TransactionServiceOptionBase<SaveTableRequest, ResultObject>
    {
        private readonly ITableInfoRepository _tableInfoRepository = null;
        private readonly IUnitOfWork _uow = null;

        public SaveTableCore(SaveTableRequest request, IUnitOfWork uow, ITableInfoRepository tableInfoRepository) 
            : base(request)
        {
            _uow = uow;
            _tableInfoRepository = tableInfoRepository;
            SetValidator(new SaveTableValidator(Request.OperationType));
        }

        protected override ResultObject Execute()
        {
            switch (Request.OperationType)
            {
                case OperationType.Add:
                    this.AddTable();
                    break;
                case OperationType.Edit:
                    this.EditTable();
                    break;
                default:
                    throw new MyFX.Core.Exceptions.AppServiceException("不支持的操作类型");
            }
            return new BatchResultObject();
        }

        #region AddTable

        private void AddTable()
        {
            bool tableExist = _tableInfoRepository.Exists(t => t.Name == Request.Name);
            if (tableExist)
            {
                throw new MyFX.Core.Exceptions.AppServiceException(string.Format("名为[{0}]的表已存在", Request.Name));
            }

            var tabInfo = new TableInfo(Request.Name, Request.Desc);
            foreach (var item in Request.ColumnInfos)
            {
                tabInfo.AddColumnInfo(item.Name, item.FormItemType, item.Desc, item.IsPrimaryKey, item.IsSystem, item.Sort);
            }

            _tableInfoRepository.Add(tabInfo);
            _uow.Commit();

            string createTableSql = GetCreateTableSql(tabInfo);
            SqlHelper.ExecuteNonQuery(createTableSql);
        }
        #endregion

        #region EditTable

        private void EditTable()
        {
            var tabInfo = _tableInfoRepository.First(t => t.Name == Request.Name);
            if (tabInfo == null)
            {
                throw new MyFX.Core.Exceptions.AppServiceException(string.Format("名为[{0}]的表不存在", Request.Name));
            }

           List<ColumnInfoDto> newColumnInfos = new List<ColumnInfoDto>();
            foreach (var item in Request.ColumnInfos)
            {
                if (tabInfo.ColumnInfos.Any(c => c.Name == item.Name))
                {
                    tabInfo.EditColumnInfo(item.Name, item.Desc, item.Sort);
                }
                else
                {
                    tabInfo.AddColumnInfo(item.Name, item.FormItemType, item.Desc, item.IsPrimaryKey, item.IsSystem, item.Sort);
                    newColumnInfos.Add(item);
                } 
            }
            _uow.Commit();

            string addColumnSql = GetAddColumnSql(tabInfo, newColumnInfos);
            if (!string.IsNullOrEmpty(addColumnSql))
            {
                SqlHelper.ExecuteNonQuery(addColumnSql);
            } 
        }

        #endregion

        #region GetCreateTableSql
        private string GetCreateTableSql(TableInfo tableInfo)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("create table [{0}]", tableInfo.Name));
            sb.AppendLine("(");
            foreach (var columnInfo in tableInfo.ColumnInfos)
            {
                sb.AppendLine(string.Format("[{0}] {1} {2},", columnInfo.Name, columnInfo.Type, columnInfo.IsPrimaryKey ? "identity(1,1) primary key" : ""));
            }
            sb.AppendLine(")");

            return sb.ToString();
        }
        #endregion

        #region GetAddColumnSql
        private string GetAddColumnSql(TableInfo tableInfo, IEnumerable<ColumnInfoDto> newColumns)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var columnInfoDto in newColumns)
            {
                var columnInfo = tableInfo.ColumnInfos.First(c => c.Name == columnInfoDto.Name);
                sb.AppendLine(string.Format("alter table [{0}] add [{1}] {2};", Request.Name, columnInfo.Name, columnInfo.Type));
            }

            return sb.ToString();
        }
        #endregion
    }
}
