using DC.Data.Request.DataManage;
using FluentValidation;

namespace DC.Service.Validators
{
    public class AddColumnValidator : EntityValidatorBase<AddColumnRequest>
    {
        public override void SetValidateRules()
        {
            RuleFor(o => o.Name).NotNull().NotEmpty();
            RuleFor(o => o.ColumnInfos).CollNotNull();
        }
    }
}
