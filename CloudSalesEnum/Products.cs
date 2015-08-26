using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CloudSalesEnum
{
    /// <summary>
    /// 产品属性类型
    /// </summary>
    public enum EnumAttrType
    {
        [DescriptionAttribute("产品参数")]
        Parameter = 1,
        [DescriptionAttribute("产品规格")]
        Specification = 2

    }
}
