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
        /// <summary>
        /// 产品属性
        /// </summary>
        [DescriptionAttribute("产品属性")]
        Parameter = 1,
        /// <summary>
        /// 产品规格
        /// </summary>
        [DescriptionAttribute("产品规格")]
        Specification = 2

    }
}
