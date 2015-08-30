using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSalesDAL
{
    public class OrganizationDAL :BaseDAL
    {
        #region 查询

        public DataSet GetC_UserByUserName(string loginname, string pwd)
        {

            SqlParameter[] paras = { 
                                    new SqlParameter("@LoginName",loginname),
                                    new SqlParameter("@LoginPwd",pwd)
                                   };
            return GetDataSet("P_GetC_UserToLogin", paras, CommandType.StoredProcedure, "User|Department|Role|Permission|Modules");
        }

        #endregion

        #region 添加
        #endregion

        #region 编辑
        #endregion

        #region 删除
        #endregion
    }
}
