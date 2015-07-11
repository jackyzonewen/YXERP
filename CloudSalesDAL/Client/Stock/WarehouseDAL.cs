using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CloudSalesDAL
{
    public class WarehouseDAL : BaseDAL
    {
        #region 查询

        public DataTable GetWarehouseTypes(string clientid)
        {
            SqlParameter[] paras = { new SqlParameter("@ClientID", clientid) };
            DataTable dt = GetDataTable("select * from C_WareHouseType where ClientID=@ClientID and Status<>9", paras, CommandType.Text);
            return dt;
        }

        #endregion

        #region 添加


        public bool AddWarehouseType(string id, string name, string description, string operateid, string clientid)
        {
            string sqlText = "INSERT INTO C_WareHouseType([TypeID] ,[TypeName],[Description],[Status],CreateUserID,ClientID) "
                                             + "values(@TypeID ,@TypeName,@Description,1,@CreateUserID,@ClientID) ";
            SqlParameter[] paras = { 
                                     new SqlParameter("@TypeID" , id),
                                     new SqlParameter("@TypeName" , name),
                                     new SqlParameter("@Description" , description),
                                     new SqlParameter("@CreateUserID" , operateid),
                                     new SqlParameter("@ClientID" , clientid)
                                   };
            return ExecuteNonQuery(sqlText, paras, CommandType.Text) > 0;
        }

        #endregion

        #region 编辑、删除

        public bool UpdateWarehouseType(string id, string name, string description)
        {
            string sqlText = "Update C_WareHouseType set [TypeName]=@TypeName,[Description]=@Description,UpdateTime=getdate() "
                           + "where [TypeID]=@TypeID";
            SqlParameter[] paras = { 
                                     new SqlParameter("@TypeID" , id),
                                     new SqlParameter("@TypeName" , name),
                                     new SqlParameter("@Description" , description)
                                   };
            return ExecuteNonQuery(sqlText, paras, CommandType.Text) > 0;
        }

        #endregion

    }
}
