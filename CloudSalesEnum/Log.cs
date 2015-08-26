using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CloudSalesEnum
{
    /// <summary>
    /// 登录类型
    /// </summary>
    public enum EnumLoginType
    {
        [DescriptionAttribute("管理后台")]
        Manage = 1,
        [DescriptionAttribute("平台系统")]
        Client = 2,
        [DescriptionAttribute("代理商系统")]
        Agents = 3
    }
}
