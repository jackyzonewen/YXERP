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
        /// 根据账号密码获取信息（登录）
        /// </summary>
        /// <param name="loginname">账号</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static M_Users GetM_UserByUserName(string loginname, string pwd, string operateip)
        {
            pwd = CloudSalesTool.Encrypt.GetEncryptPwd(pwd, loginname);
            DataTable dt = new M_UsersDAL().GetM_UserByUserName(loginname, pwd);
            M_Users model = null;
            if (dt.Rows.Count > 0)
            {
                model = new M_Users();
                model.FillData(dt.Rows[0]);
            }

            //记录登录日志
            LogBusiness.AddLoginLog(loginname, model != null, CloudSalesEnum.EnumSystemType.Manage, operateip);

            return model;
        }
    }

    #endregion
}
