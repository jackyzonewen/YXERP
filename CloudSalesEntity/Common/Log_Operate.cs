using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudSalesEntity.Log
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public class Log_Operate
    {
        public int AutoID { get; set; }

        public string UserID { get; set; }

        public string FuncName { get; set; }

        public int Type { get; set; }

        public int Modules { get; set; }

        public int Entity { get; set; }

        public string GUID { get; set; }

        public string Message { get; set; }

        public DateTime CreateTime { get; set; }

        public string OpreateIP { get; set; }

        public void FillData(System.Data.DataRow dr)
        {
            dr.FillData(this);
        }
    }
}
