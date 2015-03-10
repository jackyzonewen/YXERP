using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using CloudSalesEntity;
using CloudSalesDAL;


namespace CloudSalesBusiness
{
    #region 查询

    public class M_UsersBusiness
    {
        /// <summary>
        /// 根据用户名密码获取会员信息（登录）
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static M_Users GetM_UserByUserName(string userName, string pwd)
        {
            pwd = CloudSalesTool.Encrypt.GetEncryptPwd(pwd, userName);
            DataTable dt = new M_UsersDAL().GetM_UserByUserName(userName, pwd);
            M_Users model = null;
            if (dt.Rows.Count > 0)
            {
                model = new M_Users();
                model.FillData(dt.Rows[0]);
            }
            return model;
        }
    }

    #endregion
}
