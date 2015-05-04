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

        public DataSet GetC_UserByUserName(string userName, string pwd)
        {

            SqlParameter[] paras = { 
                                    new SqlParameter("@LoginName",userName),
                                    new SqlParameter("@LoginPwd",pwd)
                                   };
            return GetDataSet("P_GetC_UserToLogin", paras, CommandType.StoredProcedure, "User|Department|Role|Permission|Modules");
        }

        #endregion
    }
}
