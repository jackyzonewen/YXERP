using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudSalesEntity.Log
{
    /// <summary>
    /// 登录日志
    /// </summary>
    public class Log_Login
    {
        public int AutoID { get; set; }

        public string LoginName { get; set; }

        public int LoginType { get; set; }

        public int Status { get; set; }

        public DateTime CreateTime { get; set; }

        public string OpreateIP { get; set; }

        public void FillData(System.Data.DataRow dr)
        {
            dr.FillData(this);
        }
    }
}
