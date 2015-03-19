using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using CloudSalesDAL;
using CloudSalesEntity;

namespace CloudSalesBusiness
{
    public class M_ModulesBusiness
    {
        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <returns></returns>
        public static List<M_Modules> GetModules()
        {
            List<M_Modules> list = new List<M_Modules>();
            DataTable dt = new M_ModulesDAL().GetModules();
            foreach (DataRow dr in dt.Rows)
            {
                M_Modules model = new M_Modules();
                model.FillData(dr);
                list.Add(model);
            }
            return list;
        }
    }
}
