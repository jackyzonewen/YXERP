using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudSalesBusiness
{
    public class C_UserBusiness
    {
        #region 查询

        /// <summary>
        /// 客户端账号是否存在
        /// </summary>
        /// <param name="loginName">账号</param>
        /// <returns></returns>
        public static bool IsExistLoginName(string loginName)
        {
            object count = CommonBusiness.Select("C_Users", "count(0)", "LoginName='" + loginName + "'");
            return Convert.ToInt32(count) > 0;
        }

        #endregion
    }
}
