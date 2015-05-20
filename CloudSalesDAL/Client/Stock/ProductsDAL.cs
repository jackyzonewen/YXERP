using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CloudSalesDAL
{
    public class ProductsDAL : BaseDAL
    {
        #region 查询

        public DataSet GetBrandList(string keyWords, int pageSize, int pageIndex, ref int totalCount, ref int pageCount, string clientID)
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
            DataSet ds = GetDataSet("P_GetBrandList", paras, CommandType.StoredProcedure);
            totalCount = Convert.ToInt32(paras[0].Value);
            pageCount = Convert.ToInt32(paras[1].Value);
            return ds;

        }

        public DataTable GetBrandByBrandID(string brandID)
        {
            SqlParameter[] paras = { new SqlParameter("@brandID", brandID) };
            DataTable dt = GetDataTable("select * from C_Brand where BrandID=@brandID", paras, CommandType.Text);
            return dt;
        }

        public DataTable GetClientUnits(string clientid)
        {
            SqlParameter[] paras = { new SqlParameter("@ClientID", clientid) };
            DataTable dt = GetDataTable("select * from C_ProductUnit where ClientID=@ClientID and Status<>9", paras, CommandType.Text);
            return dt;
        }

        public DataSet GetAttrList(string keyWords, int pageSize, int pageIndex, ref int totalCount, ref int pageCount, string clientid)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@totalCount",SqlDbType.Int),
                                       new SqlParameter("@pageCount",SqlDbType.Int),
                                       new SqlParameter("@keyWords",keyWords),
                                       new SqlParameter("@pageSize",pageSize),
                                       new SqlParameter("@pageIndex",pageIndex),
                                       new SqlParameter("@ClientID",clientid)
                                   };
            paras[0].Value = totalCount;
            paras[1].Value = pageCount;

            paras[0].Direction = ParameterDirection.InputOutput;
            paras[1].Direction = ParameterDirection.InputOutput;
            DataSet ds = GetDataSet("P_GetProductAttrList", paras, CommandType.StoredProcedure, "Attrs|Values");
            totalCount = Convert.ToInt32(paras[0].Value);
            pageCount = Convert.ToInt32(paras[1].Value);
            return ds;

        }

        public DataSet GetProductAttrByID(string attrID)
        {
            SqlParameter[] paras = { new SqlParameter("@AttrID", attrID) };
            DataSet ds = GetDataSet("P_GetProductAttrByID", paras, CommandType.StoredProcedure, "Attrs|Values");
            return ds;
        }

        #endregion

        #region 添加

        public string AddBrand(string name, string anotherName, string icoPath, string countryCode, string cityCode, int status, string remark, string brandStyle, string operateIP, string operateID, string clientID)
        {
            string brandID = Guid.NewGuid().ToString();
            string sqlText = "INSERT INTO C_Brand([BrandID] ,[Name],[AnotherName] ,[IcoPath],[CountryCode],[CityCode],[Status],[Remark],[BrandStyle],[OperateIP],[CreateUserID],ClientID) "
                                      + "values(@BrandID ,@Name,@AnotherName ,@IcoPath,@CountryCode,@CityCode,@Status,@Remark,@BrandStyle,@OperateIP,@CreateUserID,@ClientID)";
            SqlParameter[] paras = { 
                                     new SqlParameter("@BrandID" , brandID),
                                     new SqlParameter("@Name" , name),
                                     new SqlParameter("@AnotherName" , anotherName),
                                     new SqlParameter("@IcoPath" , icoPath),
                                     new SqlParameter("@CountryCode" , countryCode),
                                     new SqlParameter("@CityCode" , cityCode),
                                     new SqlParameter("@Status" , status),
                                     new SqlParameter("@Remark" , remark),
                                     new SqlParameter("@BrandStyle" , brandStyle),
                                     new SqlParameter("@OperateIP" , operateIP),
                                     new SqlParameter("@CreateUserID" , operateID),
                                     new SqlParameter("@ClientID" , clientID)
                                   };
            return ExecuteNonQuery(sqlText, paras, CommandType.Text) > 0 ? brandID : "";

        }

        public string AddUnit(string unitName, string description, string operateid, string clientid)
        {
            string guid = Guid.NewGuid().ToString();
            string sqlText = "INSERT INTO C_ProductUnit([UnitID] ,[UnitName],[Description],CreateUserID,ClientID) "
                                            + "values(@UnitID ,@UnitName,@Description,@CreateUserID,@ClientID)";
            SqlParameter[] paras = { 
                                     new SqlParameter("@UnitID" , guid),
                                     new SqlParameter("@UnitName" , unitName),
                                     new SqlParameter("@Description" , description),
                                     new SqlParameter("@CreateUserID" , operateid),
                                     new SqlParameter("@ClientID" , clientid)
                                   };
            if (ExecuteNonQuery(sqlText, paras, CommandType.Text) > 0)
            {
                return guid;
            }
            return "";
        }

        public bool AddProductAttr(string attrID, string attrName, string description, string operateid, string clientid)
        {
            string sqlText = "INSERT INTO C_ProductAttr([AttrID] ,[AttrName],[Description],[Status],CreateUserID,ClientID) "
                                             + "values(@AttrID ,@AttrName,@Description,1,@CreateUserID,@ClientID) ";
            SqlParameter[] paras = { 
                                     new SqlParameter("@AttrID" , attrID),
                                     new SqlParameter("@AttrName" , attrName),
                                     new SqlParameter("@Description" , description),
                                     new SqlParameter("@CreateUserID" , operateid),
                                     new SqlParameter("@ClientID" , clientid)
                                   };
            return ExecuteNonQuery(sqlText, paras, CommandType.Text) > 0;
        }

        public bool AddAttrValue(string valueID, string valueName, string attrID, string operateid, string clientid)
        {
            string sqlText = "INSERT INTO C_AttrValue([ValueID] ,[ValueName],[Status],[AttrID],CreateUserID,ClientID) "
                                             + "values(@ValueID ,@ValueName,1,@AttrID,@CreateUserID,@ClientID) ";
            SqlParameter[] paras = { 
                                     new SqlParameter("@ValueID" , valueID),
                                     new SqlParameter("@ValueName" , valueName),
                                     new SqlParameter("@AttrID" , attrID),
                                     new SqlParameter("@CreateUserID" , operateid),
                                     new SqlParameter("@ClientID" , clientid)
                                   };
            return ExecuteNonQuery(sqlText, paras, CommandType.Text) > 0;
        }

        #endregion

        #region 编辑

        public bool UpdateBrand(string brandID, string name, string anotherName, string countryCode, string cityCode, int status, string remark, string brandStyle, string operateIP, string operateID)
        {
            string sqlText = "Update C_Brand set [Name]=@Name,[AnotherName]=@AnotherName ,[CountryCode]=@CountryCode,[CityCode]=@CityCode," +
                "[Status]=@Status,[Remark]=@Remark,[BrandStyle]=@BrandStyle,[UpdateTime]=getdate() where [BrandID]=@BrandID";
            SqlParameter[] paras = { 
                                     new SqlParameter("@Name" , name),
                                     new SqlParameter("@AnotherName" , anotherName),
                                     new SqlParameter("@CountryCode" , countryCode),
                                     new SqlParameter("@CityCode" , cityCode),
                                     new SqlParameter("@Status" , status),
                                     new SqlParameter("@Remark" , remark),
                                     new SqlParameter("@BrandStyle" , brandStyle),
                                     new SqlParameter("@BrandID" , brandID),
                                   };
            return ExecuteNonQuery(sqlText, paras, CommandType.Text) > 0;
        }

        public bool UpdateUnit(string unitID, string unitName, string description)
        {
            string sqlText = "Update C_ProductUnit set [UnitName]=@UnitName,[Description]=@Description,UpdateTime=getdate()  where [UnitID]=@UnitID";
            SqlParameter[] paras = { 
                                     new SqlParameter("@UnitID",unitID),
                                     new SqlParameter("@UnitName" , unitName),
                                     new SqlParameter("@Description" , description)
                                   };
            return ExecuteNonQuery(sqlText, paras, CommandType.Text) > 0;
        }

        public bool UpdateUnitStatus(string unitid, int status)
        {
            string sqlText = "Update C_ProductUnit set Status=@Status,UpdateTime=getdate()  where [UnitID]=@UnitID";
            SqlParameter[] paras = { 
                                     new SqlParameter("@UnitID",unitid),
                                     new SqlParameter("@Status" , status)
                                   };
            return ExecuteNonQuery(sqlText, paras, CommandType.Text) > 0;
        }

        public bool UpdateProductAttr(string attrID, string attrName, string description)
        {
            string sqlText = "Update C_ProductAttr set [AttrName]=@AttrName,[Description]=@Description  where [AttrID]=@AttrID";
            SqlParameter[] paras = { 
                                     new SqlParameter("@AttrID",attrID),
                                     new SqlParameter("@AttrName" , attrName),
                                     new SqlParameter("@Description" , description),
                                   };
            return ExecuteNonQuery(sqlText, paras, CommandType.Text) > 0;
        }

        public bool UpdateAttrValue(string ValueID, string ValueName)
        {
            string sqlText = "Update C_AttrValue set [ValueName]=@ValueName  where [ValueID]=@ValueID";
            SqlParameter[] paras = { 
                                     new SqlParameter("@ValueID",ValueID),
                                     new SqlParameter("@ValueName" , ValueName),
                                   };
            return ExecuteNonQuery(sqlText, paras, CommandType.Text) > 0;
        }

        public bool UpdateProductAttrStatus(string attrid, int status)
        {
            string sqlText = "Update C_ProductAttr set Status=@Status,UpdateTime=getdate()  where [AttrID]=@AttrID";
            SqlParameter[] paras = { 
                                     new SqlParameter("@AttrID",attrid),
                                     new SqlParameter("@Status" , status)
                                   };
            return ExecuteNonQuery(sqlText, paras, CommandType.Text) > 0;
        }

        public bool UpdateAttrValueStatus(string valueid, int status)
        {
            string sqlText = "Update C_AttrValue set Status=@Status,UpdateTime=getdate()  where [ValueID]=@ValueID";
            SqlParameter[] paras = { 
                                     new SqlParameter("@ValueID",valueid),
                                     new SqlParameter("@Status" , status)
                                   };
            return ExecuteNonQuery(sqlText, paras, CommandType.Text) > 0;
        }
        #endregion
    }
}
