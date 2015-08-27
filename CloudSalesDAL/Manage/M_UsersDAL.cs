using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CloudSalesDAL
{
    public class M_UsersDAL : BaseDAL
    {
        #region 查询

        public DataTable GetM_UserByUserName(string loginname, string pwd)
        {

            SqlParameter[] paras = { 
                                    new SqlParameter("@UserName",loginname),
                                    new SqlParameter("@LoginPwd",pwd)
                                   };
            return GetDataTable("select * from M_Users where LoginName=@UserName and LoginPwd=@LoginPwd", paras, CommandType.Text);
        }

        #endregion
    }
}
