using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Common;
using DC.Data.Request.DataManage;
using FluentValidation;

namespace DC.Service.Validators
{
    public class SaveTableValidator : EntityValidatorBase<SaveTableRequest>
    {
        private readonly OperationType _operationType;

        public SaveTableValidator(OperationType operationType)
        {
            _operationType = operationType;
        }

        public override void SetValidateRules()
        {
            if (_operationType == OperationType.Add)
            {
                RuleFor(o => o.Name).NotNull().NotEmpty();
            }
           
            RuleFor(o => o.Desc).NotNull().NotEmpty();
            RuleFor(o => o.ColumnInfos).CollNotNull();
        }
    }
}
