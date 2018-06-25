using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.Common.DataManage
{
    /// <summary>
    /// 表单项类型
    /// </summary>
    public enum FormItemType : byte
    {
        /// <summary>
        /// 普通文本
        /// </summary>
        [Display(Name = "普通文本")]
        Text = 1,

        /// <summary>
        /// 长文本
        /// </summary>
        [Display(Name = "长文本")]
        BigText = 5,

        /// <summary>
        /// 整数
        /// </summary>
        [Display(Name = "整数")]
        Number = 10,

        /// <summary>
        /// 小数
        /// </summary>
        [Display(Name = "小数")]
        Double = 15,

        /// <summary>
        /// 货币
        /// </summary>
        [Display(Name = "货币")]
        Money = 25,

        /// <summary>
        /// 单项选择
        /// </summary>
        [Display(Name = "单项选择")]
        RadioButtonList = 30,

        /// <summary>
        /// 多项选择
        /// </summary>
        [Display(Name = "多项选择")]
        CheckBoxList = 35,

        /// <summary>
        /// 下拉选择
        /// </summary>
        [Display(Name = "下拉选择")]
        DropDownList = 40,

        /// <summary>
        /// 时间
        /// </summary>
        [Display(Name = "时间")]
        DateTime = 45,

        /// <summary>
        /// 图片
        /// </summary>
        [Display(Name = "图片")]
        Image = 50,

        /// <summary>
        /// 附件
        /// </summary>
        [Display(Name = "附件")]
        File = 55
    }
}
