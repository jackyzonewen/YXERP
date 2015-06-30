using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudSalesEnum
{
    /// <summary>
    /// 菜单类型
    /// </summary>
    public enum MenuType
    {
        /// <summary>
        /// 管理系统
        /// </summary>
        Manage = 1,
        /// <summary>
        /// 客户端（公司）
        /// </summary>
        Client = 2,
        /// <summary>
        /// 代理商
        /// </summary>
        Agent = 3
    }

    /// <summary>
    /// 状态枚举
    /// </summary>
    public enum StatusEnum
    {
        //所有（搜索）
        All = -1,
        //无效（未审核）
        Invalid = 0,
        //有效(上架、已审核)
        Valid = 1,
        //删除
        Delete = 9
    }

}
