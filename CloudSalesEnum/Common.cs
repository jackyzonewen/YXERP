using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CloudSalesEnum
{
    /// <summary>
    /// 系统类型
    /// </summary>
    public enum EnumSystemType
    {
        [DescriptionAttribute("管理系统")]
        Manage = 1,
        [DescriptionAttribute("客户端（公司）")]
        Client = 2,
        [DescriptionAttribute("代理商")]
        Agent = 3
    }

    /// <summary>
    /// 状态枚举
    /// </summary>
    public enum EnumStatus
    {
        [DescriptionAttribute("全部")]
        All = -1,
        [DescriptionAttribute("禁用")]
        Invalid = 0,
        [DescriptionAttribute("启用")]
        Valid = 1,
        [DescriptionAttribute("删除")]
        Delete = 9
    }
    /// <summary>
    /// 执行状态码
    /// </summary>
    public enum EnumResultStatus
    {
        [DescriptionAttribute("失败")]
        Failed = 0,
        [DescriptionAttribute("成功")]
        Success = 1,
        [DescriptionAttribute("错误")]
        Error = 10001,
        [DescriptionAttribute("已存在")]
        Exists = 10002
    }

}
