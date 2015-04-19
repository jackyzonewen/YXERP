using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using CloudSalesEntity;
using CloudSalesDAL;

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

        /// <summary>
        /// 根据用户名密码获取会员信息（登录）
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static C_Users GetC_UserByUserName(string userName, string pwd)
        {
            pwd = CloudSalesTool.Encrypt.GetEncryptPwd(pwd, userName);
            DataTable dt = new C_UsersDAL().GetC_UserByUserName(userName, pwd);
            C_Users model = null;
            if (dt.Rows.Count > 0)
            {
                model = new C_Users();
                model.FillData(dt.Rows[0]);
            }
            return model;
        }

        #endregion
    }
}
