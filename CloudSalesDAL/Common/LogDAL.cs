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
        public static Task<bool> AddLoginLog(string loginname, int status, int systemtype, string operateip)
        {
            string sqlText = "insert into Log_Login(LoginName,SystemType,Status,CreateTime,OperateIP) values(@LoginName,@SystemType,@Status,GETDATE(),@OperateIP)";
            SqlParameter[] paras = { 
                                     new SqlParameter("@LoginName" , loginname),
                                     new SqlParameter("@SystemType" , systemtype),
                                     new SqlParameter("@Status" , status),
                                     new SqlParameter("@OperateIP" , operateip)
                                   };
            return Task.Run(() => { return ExecuteNonQuery(sqlText, paras, CommandType.Text) > 0; });
        }

        public static Task<bool> AddOperateLog(string userid, string funcname, int type, int modules, int entity, string guid, string message, string operateip)
        {
            string sqlText = "insert into Log_Operate(UserID,FuncName,Type,Modules,Entity,GUID,Message,CreateTime,OperateIP) " +
                            " values(@UserID,@FuncName,@Type,@Modules,@Entity,@GUID,@Message,GETDATE(),@OperateIP)";
            SqlParameter[] paras = { 
                                     new SqlParameter("@UserID" , userid),
                                     new SqlParameter("@FuncName" , funcname),
                                     new SqlParameter("@Type" , type),
                                     new SqlParameter("@Modules" , modules),
                                     new SqlParameter("@Entity" , entity),
                                     new SqlParameter("@GUID" , guid),
                                     new SqlParameter("@Message" , message),
                                     new SqlParameter("@OperateIP" , operateip)
                                   };
            return Task.Run(() => { return ExecuteNonQuery(sqlText, paras, CommandType.Text) > 0; });
        }

        public static Task<bool> AddErrorLog(string userid, string message, int systemtype, string operateip)
        {
            string sqlText = "insert into Log_Error(UserID,Message,SystemType,CreateTime,OperateIP) values(@UserID,@Message,@SystemType,GETDATE(),@OperateIP)";
            SqlParameter[] paras = { 
                                     new SqlParameter("@UserID" , userid),
                                     new SqlParameter("@Message" , message),
                                     new SqlParameter("@SystemType" , systemtype),
                                     new SqlParameter("@OperateIP" , operateip)
                                   };
            return Task.Run(() => { return ExecuteNonQuery(sqlText, paras, CommandType.Text) > 0; });
        }
    }
}
