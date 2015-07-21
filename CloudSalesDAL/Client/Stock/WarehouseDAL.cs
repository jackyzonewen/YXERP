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

        public DataSet GetWareHouses(string keyWords, int pageSize, int pageIndex, ref int totalCount, ref int pageCount, string clientID)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@totalCount",SqlDbType.Int),
                                       new SqlParameter("@pageCount",SqlDbType.Int),
                                       new SqlParameter("@keyWords",keyWords),
                                       new SqlParameter("@pageSize",pageSize),
                                       new SqlParameter("@pageIndex",pageIndex),
                                       new SqlParameter("@ClientID",clientID)
                                       
                                   };
            paras[0].Value = totalCount;
            paras[1].Value = pageCount;

            paras[0].Direction = ParameterDirection.InputOutput;
            paras[1].Direction = ParameterDirection.InputOutput;
            DataSet ds = GetDataSet("P_GetWareHouses", paras, CommandType.StoredProcedure);
            totalCount = Convert.ToInt32(paras[0].Value);
            pageCount = Convert.ToInt32(paras[1].Value);
            return ds;

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

        public bool AddWareHouse(string id, string warecode, string name, string shortname, string citycode, int status,string description, string operateid, string clientid)
        {
            string sqlText = "insert into C_WareHouse(WareID,WareCode,Name,ShortName,CityCode,Status,Description,CreateUserID,ClientID) "+
                                            " values(@WareID,@WareCode,@Name,@ShortName,@CityCode,@Status,@Description,@CreateUserID,@ClientID) ";
            SqlParameter[] paras = { 
                                     new SqlParameter("@WareID" , id),
                                     new SqlParameter("@WareCode" , warecode),
                                     new SqlParameter("@Name" , name),
                                     new SqlParameter("@ShortName" , shortname),
                                     new SqlParameter("@CityCode" , citycode),
                                     new SqlParameter("@Status" , status),
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

        public bool UpdateWareHouse(string id, string name, string shortname, string citycode, int status, string description)
        {
            string sqlText = "Update C_WareHouse set Name=@Name,ShortName=@ShortName,CityCode=@CityCode,Status=@Status,Description=@Description,UpdateTime=getdate() " +
                            "  where WareID=@WareID ";
            SqlParameter[] paras = { 
                                     new SqlParameter("@WareID" , id),
                                     new SqlParameter("@Name" , name),
                                     new SqlParameter("@ShortName" , shortname),
                                     new SqlParameter("@CityCode" , citycode),
                                     new SqlParameter("@Status" , status),
                                     new SqlParameter("@Description" , description)
                                   };
            return ExecuteNonQuery(sqlText, paras, CommandType.Text) > 0;
        }

        #endregion

    }
}
