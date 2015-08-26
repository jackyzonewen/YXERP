using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSalesDAL
{
    public class LogDAL :BaseDAL
    {
        public static Task<bool> AddLoginLog(string loginname, int status, int logintype, string operateip)
        {
            string sqlText = "insert into Log_Login(LoginName,LoginType,Status,CreateTime,OperateIP) values(@LoginName,@LoginType,@Status,GETDATE(),@OperateIP)";
            SqlParameter[] paras = { 
                                     new SqlParameter("@LoginName" , loginname),
                                     new SqlParameter("@LoginType" , logintype),
                                     new SqlParameter("@Status" , status),
                                     new SqlParameter("@OperateIP" , operateip)
                                   };
            return Task.Run(() => { return ExecuteNonQuery(sqlText, paras, CommandType.Text) > 0; });
        }
    }
}
