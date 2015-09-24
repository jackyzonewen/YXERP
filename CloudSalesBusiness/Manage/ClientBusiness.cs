using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using CloudSalesEntity;
using CloudSalesDAL;
using CloudSalesTool;


namespace CloudSalesBusiness
{
    public class ClientBusiness
    {
        #region 查询

        /// <summary>
        /// 获取客户端列表
        /// </summary>
        /// <param name="keyWords"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public static List<Clients> GetClients(string keyWords, int pageSize, int pageIndex, ref int totalCount, ref int pageCount)
        {
            DataTable dt = CommonBusiness.GetPagerData("Clients", "*", "Status<>9", "AutoID", pageSize, pageIndex, out totalCount, out pageCount);
            List<Clients> list = new List<Clients>();
            Clients model;
            foreach (DataRow item in dt.Rows)
            {
                model = new Clients();
                model.FillData(item);
                model.City = CommonCache.Citys.Where(c => c.CityCode == model.CityCode).FirstOrDefault();
                model.IndustryEntity = IndustryBusiness.GetIndustryByClientID(AppSettings.Settings[AppSettingsWEB.Manage, "ClientID"]).Where(i => i.IndustryID.ToLower() == model.Industry.ToLower()).FirstOrDefault();
                list.Add(model);
            }

            return list;
        }

        public static Clients GetClientDetail(string clientID)
        {
            DataTable dt = ClientDAL.BaseProvider.GetClientDetail(clientID);
            Clients model = new Clients();
            if (dt.Rows.Count==1)
            {
                DataRow row=dt.Rows[0];
                model.FillData(row);
                model.City = CommonCache.Citys.Where(c => c.CityCode == model.CityCode).FirstOrDefault();
                model.IndustryEntity = IndustryBusiness.GetIndustryByClientID(AppSettings.Settings[AppSettingsWEB.Manage, "ClientID"]).Where(i => i.IndustryID.ToLower() == model.Industry.ToLower()).FirstOrDefault();
                model.Modules = ModulesBusiness.GetModulesByClientID(clientID);
            }

            return model;
        }
        #endregion

        #region 添加

        /// <summary>
        /// 添加客户端
        /// </summary>
        /// <param name="model">Clients 对象</param>
        /// <param name="loginName">账号</param>
        /// <param name="loginPwd">密码</param>
        /// <param name="userid">操作人</param>
        /// <param name="result">0：失败 1：成功 2：账号已存在 3：模块未选择</param>
        /// <returns></returns>
        public static string InsertClient(Clients model, string loginName, string loginPwd, string userid, out int result)
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
            string clientid = ClientDAL.BaseProvider.InsertClient(model.CompanyName, model.ContactName, model.MobilePhone, model.Industry, model.CityCode,
                                                             model.Address, model.Description, loginName, loginPwd, modules, userid, out result);
            return clientid;
        }

        #endregion
        public static bool DeleteClient(string clientID)
        {
            return CommonBusiness.Update("Clients", "Status", 9, " ClientID='" + clientID + "'");
        }
        #region  编辑
        public static bool UpdateClient(Clients model, string loginName, string loginPwd, string userid, out int result)
        {
            string modules = "";
            foreach (var item in model.Modules)
            {
                modules += item.ModulesID + ",";
            }
            if (modules == "")
            {
                result = 3;
                return false;
            }
            loginPwd = CloudSalesTool.Encrypt.GetEncryptPwd(loginPwd, loginName);
            modules = modules.Substring(0, modules.Length - 1);

            result = 1;
            return ClientDAL.BaseProvider.UpdateClient(model.ClientID, model.CompanyName
                , model.ContactName, model.MobilePhone, model.Industry
                , model.CityCode, model.Address, model.Description
                , loginName, loginPwd, modules, userid);

        }

        public static bool ClientAuthorize(string clientID, int userQuantity, int authorizeType, DateTime endTime)
        {
            return ClientDAL.BaseProvider.ClientAuthorize(clientID, userQuantity, authorizeType, endTime);
        }
        #endregion

    }
}
