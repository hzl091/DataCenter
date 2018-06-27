/****************************************************************************************
 * 文件名：GetPagerDataValidator
 * 作者：huangzl
 * 创始时间：2018/6/27 18:54:04
 * 创建说明：
****************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DC.Data.Request.DataManage;
using FluentValidation;

namespace DC.Service.Validators
{
    public class GetPagerDataValidator : EntityValidatorBase<GetPagerDataRequest>
    {
        public override void SetValidateRules()
        {
            RuleFor(o => o.TableName).NotNull().NotEmpty();
            RuleFor(o => o.PageIndex).GreaterThanOrEqualTo(0);
            RuleFor(o => o.PageSize).GreaterThan(0);
        }
    }
}
