using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Data.Request.DataManage;
using DC.DAL.IRepository;
using DC.Service.Validators;
using MyFX.Core.Actions;
using MyFX.Core.BaseModel.Result;
using MyFX.Core.Domain.Uow;

namespace DC.Service.Core
{
    public class ColumnMoveCore : ServiceOptionBase<ColumnMoveRequest, ResultObject>
    {
        private readonly ITableInfoRepository _tableInfoRepository = null;
        private readonly IUnitOfWork _uow = null;

        public ColumnMoveCore(ColumnMoveRequest request, IUnitOfWork uow, ITableInfoRepository tableInfoRepository)
            : base(request)
        {
            _uow = uow;
            _tableInfoRepository = tableInfoRepository;
            SetValidator(new ColumnMoveValidator());
        }

        protected override ResultObject Execute()
        {
            var tabInfo = _tableInfoRepository.First(t => t.Name == Request.TabName);
            if (tabInfo == null)
            {
                throw new MyFX.Core.Exceptions.AppServiceException(string.Format("名为[{0}]的表不存在", Request.TabName));
            }

            tabInfo.ColumnMove(Request.ColName, Request.MoveType);
            _uow.Commit();
            return new ResultObject();
        }
    }
}
