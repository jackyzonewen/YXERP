using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CloudSalesDAL;
using System.Threading.Tasks;
using CloudSalesEnum;

namespace CloudSalesBusiness
{
    public class LogBusiness
    {
        /// <summary>
        /// 添加登录日志
        /// </summary>
        /// <param name="loginname">用户名</param>
        /// <param name="status">登录结果</param>
        /// <param name="logintype">系统类型</param>
        /// <param name="operateip">登录IP</param>
        public static async void AddLoginLog(string loginname, bool status, EnumLoginType logintype, string operateip)
        {
            await LogDAL.AddLoginLog(loginname, status ? 1 : 0, (int)logintype, operateip);
        }
    }
}
