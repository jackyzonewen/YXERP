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
        /// <param name="loginname">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static C_Users GetC_UserByUserName(string loginname, string pwd, string operateip)
        {
            pwd = CloudSalesTool.Encrypt.GetEncryptPwd(pwd, loginname);
            DataSet ds = new C_UsersDAL().GetC_UserByUserName(loginname, pwd);
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
                    foreach (DataRow dr in ds.Tables["Modules"].Rows)
                    {
                        M_Modules module = new M_Modules();
                        module.FillData(dr);
                        if (modules.ContainsKey(module.ModulesID))
                        {
                            foreach (var item in modules[module.ModulesID])
                            {
                                if (list.Where(m => m.MenuCode == item.MenuCode).Count() == 0)
                                {
                                    list.Add(item);
                                }
                            }
                        }
                    }
                    list = list.OrderBy(m => m.Sort).ToList();
                    CommonCache.ClientMenus.Add(model.ClientID, list);
                    model.Menus = list;
                }
            }

            //记录登录日志
            LogBusiness.AddLoginLog(loginname, model != null, CloudSalesEnum.EnumSystemType.Client, operateip);

            return model;
        }

        #endregion
    }
}
