using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CloudSalesDAL
{
    public class C_UsersDAL : BaseDAL
    {
        #region 查询

        public DataTable GetC_UserByUserName(string userName, string pwd)
        {

            SqlParameter[] paras = { 
                                    new SqlParameter("@UserName",userName),
                                    new SqlParameter("@LoginPwd",pwd)
                                   };
            return GetDataTable("select * from C_Users where LoginName=@UserName and LoginPwd=@LoginPwd", paras, CommandType.Text);
        }

        #endregion
    }
}
