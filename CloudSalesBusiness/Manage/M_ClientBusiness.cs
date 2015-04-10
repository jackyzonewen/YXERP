using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CloudSalesEntity;
using CloudSalesDAL;

namespace CloudSalesBusiness
{
    public class M_ClientBusiness
    {
        #region 查询
        #endregion

        #region 添加

        /// <summary>
        /// 添加客户端
        /// </summary>
        /// <param name="model">M_Clients 对象</param>
        /// <param name="loginName">账号</param>
        /// <param name="loginPwd">密码</param>
        /// <param name="userid">操作人</param>
        /// <param name="result">0：失败 1：成功 2：账号已存在 3：模块未选择</param>
        /// <returns></returns>
        public static string InsertClient(M_Clients model, string loginName, string loginPwd, string userid, out int result)
        {
            string modules = "";
            foreach (var item in model.Modules)
            {
                modules += item.ModulesID + ",";
            }
            if (modules == "")
            {
                result = 3;
                return "";
            }
            loginPwd = CloudSalesTool.Encrypt.GetEncryptPwd(loginPwd, loginName);
            modules = modules.Substring(0, modules.Length - 1);
            string clientid = new M_ClientDAL().InsertClient(model.CompanyName, model.ContactName, model.MobilePhone, model.Industry, model.CityCode,
                                                             model.Address, model.Description, loginName, loginPwd, loginName, modules, out result);
            return clientid;
        }

        #endregion

        #region 编辑
        #endregion

    }
}
