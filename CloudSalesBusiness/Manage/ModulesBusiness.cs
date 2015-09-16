using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using CloudSalesDAL;
using CloudSalesEntity;

namespace CloudSalesBusiness
{
    public class ModulesBusiness
    {
        /// <summary>
        /// 获取模块列表
        /// </summary>
        /// <returns></returns>
        public static List<Modules> GetModules()
        {
            List<Modules> list = new List<Modules>();
            DataTable dt = new ModulesDAL().GetModules();
            foreach (DataRow dr in dt.Rows)
            {
                Modules model = new Modules();
                model.FillData(dr);
                list.Add(model);
            }
            return list;
        }
    }
}
