using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CloudSalesDAL;
using CloudSalesEntity;
using System.Data;

namespace CloudSalesBusiness
{
    public class C_IndustryBusiness
    {

        #region 查询

        /// <summary>
        /// 获取行业列表
        /// </summary>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public static List<C_Industry> GetIndustryByClientID(string clientid)
        {
            List<C_Industry> list = new List<C_Industry>();
            DataTable dt = new C_IndustryDAL().GetIndustryByClientID(clientid);
            foreach (DataRow item in dt.Rows)
            {
                C_Industry model = new C_Industry();
                model.FillData(item);
                list.Add(model);
            }
            return list;
        }

        #endregion

        #region 添加

        /// <summary>
        /// 添加行业
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="description">描述</param>
        /// <param name="userid">操作人</param>
        /// <param name="clientid">客户端ID</param>
        /// <returns>行业ID</returns>
        public string InsertIndustry(string name, string description, string userid, string clientid)
        {
            return new C_IndustryDAL().InsertIndustry(name, description, userid, clientid);
        }

        #endregion
    }
}
