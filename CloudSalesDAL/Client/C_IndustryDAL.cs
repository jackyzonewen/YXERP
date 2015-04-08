using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CloudSalesDAL
{
    public class C_IndustryDAL : BaseDAL
    {
        #region 查询

        public DataTable GetIndustryByClientID(string clientid)
        {

            SqlParameter[] paras = { 
                                    new SqlParameter("@ClientID",clientid)
                                   };
            return GetDataTable("select * from C_Industry where ClientID=@ClientID", paras, CommandType.Text);
        }

        #endregion

        #region 添加

        public string InsertIndustry(string name, string description, string userid, string clientid)
        {
            string industryID = Guid.NewGuid().ToString();
            string sql = "Insert into C_Industry(IndustryID,Name,Description,CreateUserID,ClientID) values(@IndustryID,@Name,@Description,@CreateUserID,@ClientID)";
            SqlParameter[] paras = { 
                                       new SqlParameter("@IndustryID",industryID),
                                       new SqlParameter("@Name",name),
                                       new SqlParameter("@Description",description),
                                       new SqlParameter("@CreateUserID",string.IsNullOrEmpty(userid) ? (object)DBNull.Value : userid),
                                       new SqlParameter("@ClientID",clientid)
                                   };

            return ExecuteNonQuery(sql, paras, CommandType.Text) > 0 ? industryID : "";
        }

        #endregion
    }
}
