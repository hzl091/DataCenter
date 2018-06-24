using DC.Data.Request.DataManage;
using DC.DAL.IRepository;
using DC.Domain.DataManage;
using DC.Service.Validators;
using MyFX.Core.Actions;
using MyFX.Core.BaseModel.Result;
using MyFX.Core.Domain.Uow;

namespace DC.Service.Core
{
    public class CreateTableCore : ServiceOptionBase<CreateTableRequest, ResultObject>
    {
        private readonly ITableInfoRepository _tableInfoRepository = null;
        private readonly IUnitOfWork _uow = null;
        

        public CreateTableCore(CreateTableRequest request, IUnitOfWork uow, ITableInfoRepository tableInfoRepository) 
            : base(request)
        {
            _uow = uow;
            _tableInfoRepository = tableInfoRepository;
            SetValidator(new CreateTableValidator());
        }

        protected override ResultObject Execute()
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
            
            return new BatchResultObject();
        }
    }
}
