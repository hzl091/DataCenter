using DC.Data.Request.DataManage;
using FluentValidation;

namespace DC.Service.Validators
{
    public class CreateTableValidator : EntityValidatorBase<CreateTableRequest>
    {
        public override void SetValidateRules()
        {
            RuleFor(o => o.Name).NotNull().NotEmpty();
            RuleFor(o => o.Desc).NotNull().NotEmpty();
            RuleFor(o => o.ColumnInfos).CollNotNull();
        }
    }
}
