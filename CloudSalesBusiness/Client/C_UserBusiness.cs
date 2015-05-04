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
            DataSet ds = new C_UsersDAL().GetC_UserByUserName(userName, pwd);
            C_Users model = null;
            if (ds.Tables.Contains("User") && ds.Tables["User"].Rows.Count > 0)
            {
                model = new C_Users();
                model.FillData(ds.Tables["User"].Rows[0]);

                if (CommonCache.ClientMenus.ContainsKey(model.ClientID))
                {
                    model.Menus = CommonCache.ClientMenus[model.ClientID];
                }
                else if (ds.Tables.Contains("Modules"))
                {
                    List<Menu> list = new List<Menu>();
                    var modules = CommonCache.Modules;
                    foreach (DataRow module in ds.Tables["Modules"].Rows)
                    {
                        if (modules.ContainsKey(module["ModulesID"].ToString()))
                        {
                            foreach (var item in modules[module["ModulesID"].ToString()])
                            {
                                if (list.Where(m => m.MenuCode == item.MenuCode).Count() == 0)
                                {
                                    list.Add(item);
                                }
                            }
                        }
                    }
                    CommonCache.ClientMenus.Add(model.ClientID, list);
                    model.Menus = list;
                }
            }
            return model;
        }

        #endregion
    }
}
