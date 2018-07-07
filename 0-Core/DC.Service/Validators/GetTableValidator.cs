using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Data.Request.DataManage;
using FluentValidation;

namespace DC.Service.Validators
{
    public class GetTableValidator : EntityValidatorBase<GetTableRequest>
    {
        public override void SetValidateRules()
        {
            RuleFor(o => o.TableName).NotNull().NotEmpty();
        }
    }
}
