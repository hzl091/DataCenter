using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Data.Request.DataManage;
using FluentValidation;

namespace DC.Service.Validators
{
    public class ColumnMoveValidator : EntityValidatorBase<ColumnMoveRequest>
    {
        public override void SetValidateRules()
        {
            RuleFor(o => o.TabName).NotNull().NotEmpty();
            RuleFor(o => o.ColName).CollNotNull();
        }
    }
}
