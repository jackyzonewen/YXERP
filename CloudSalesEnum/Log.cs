using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CloudSalesEnum
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum EnumLogType
    {
        Create = 1,
        Update = 2,
        Delete = 3
    }
    /// <summary>
    /// 日志模块
    /// </summary>
    public enum EnumLogModules
    {
        [DescriptionAttribute("库存")]
        Stock = 1
    }
    /// <summary>
    /// 日志对象
    /// </summary>
    public enum EnumLogEntity
    {
        Brand = 1,
        ProductUnit
    }
}
