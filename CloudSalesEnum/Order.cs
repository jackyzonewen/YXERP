using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CloudSalesEnum
{
    /// <summary>
    /// 单据类型
    /// </summary>
    public enum EnumDocType
    {
        /// <summary>
        /// 全部（仅供查询）
        /// </summary>
        All = -1,
        [DescriptionAttribute("采购单")]
        RK = 1,
        [DescriptionAttribute("出库单")]
        CK = 2
    }
    /// <summary>
    /// 订单类型（冗余单据类型）
    /// </summary>
    public enum EnumOrderType
    {
        /// <summary>
        /// 全部（仅供查询）
        /// </summary>
        All = -1,
        [DescriptionAttribute("采购单")]
        RK = 1,
        [DescriptionAttribute("出库单")]
        CK = 2
    }
    /// <summary>
    /// 单据状态
    /// </summary>
    public enum EnumDocStatus
    {
        /// <summary>
        /// 全部（仅供查询）
        /// </summary>
        All = -1,
        [DescriptionAttribute("未处理")]
        Normal = 0,
        [DescriptionAttribute("部分审核")]
        AuditPart = 1,
        [DescriptionAttribute("已审核")]
        AuditAll = 2,
        [DescriptionAttribute("已作废")]
        Invalid = 4,
        [DescriptionAttribute("已删除")]
        Delete = 9
    }

}
