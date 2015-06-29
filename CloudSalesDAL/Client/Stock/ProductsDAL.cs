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

        public DataTable GetBrandList(string clientID)
        {
            SqlParameter[] paras = { new SqlParameter("@clientID", clientID) };
            DataTable dt = GetDataTable("select BrandID,Name from C_Brand where ClientID=@clientID and Status<>9", paras, CommandType.Text);
            return dt;

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

        public DataTable GetAttrList(string clientid)
        {
            SqlParameter[] paras = { new SqlParameter("@ClientID", clientid) };

            return GetDataTable("select AttrID,AttrName,Description from C_ProductAttr where ClientID=@ClientID and Status<>9", paras, CommandType.Text);

        }

        public DataSet GetProductAttrByID(string attrID)
        {
            SqlParameter[] paras = { new SqlParameter("@AttrID", attrID) };
            DataSet ds = GetDataSet("P_GetProductAttrByID", paras, CommandType.StoredProcedure, "Attrs|Values");
            return ds;
        }

        public DataTable GetChildCategorysByID(string categoryid)
        {
            SqlParameter[] paras = { new SqlParameter("@PID", categoryid) };
            DataTable dt = GetDataTable("select * from C_Category where PID=@PID Order by CreateTime", paras, CommandType.Text);
            return dt;
        }

        public DataTable GetCategoryByID(string categoryid)
        {
            SqlParameter[] paras = { new SqlParameter("@CategoryID", categoryid) };
            DataTable dt = GetDataTable("select * from C_Category where CategoryID=@CategoryID", paras, CommandType.Text);
            return dt;
        }

        public DataSet GetCategoryDetailByID(string categoryid)
        {
            SqlParameter[] paras = { new SqlParameter("@CategoryID", categoryid) };
            DataSet ds = GetDataSet("P_GetCategoryDetailByID", paras, CommandType.StoredProcedure, "Category|Attrs|Values");
            return ds;
        }

        public DataSet GetProductList(string keyWords, int pageSize, int pageIndex, ref int totalCount, ref int pageCount, string clientID)
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
            DataSet ds = GetDataSet("P_GetProductList", paras, CommandType.StoredProcedure);
            totalCount = Convert.ToInt32(paras[0].Value);
            pageCount = Convert.ToInt32(paras[1].Value);
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

        public string AddCategory(string categoryCode, string categoryName, string pid, int status, string attrlist, string saleattr, string description, string operateid, string clientid)
        {
            string id = "";
            SqlParameter[] paras = { 
                                       new SqlParameter("@CategoryID",SqlDbType.NVarChar,64),
                                       new SqlParameter("@CategoryCode",categoryCode),
                                       new SqlParameter("@CategoryName",categoryName),
                                       new SqlParameter("@PID",pid),
                                       new SqlParameter("@Status",status),
                                       new SqlParameter("@AttrList",attrlist),
                                       new SqlParameter("@SaleAttr",saleattr),
                                       new SqlParameter("@Description",description),
                                       new SqlParameter("@CreateUserID",operateid),
                                       new SqlParameter("@ClientID",clientid)
                                   };
            paras[0].Value = id;
            paras[0].Direction = ParameterDirection.InputOutput;

            ExecuteNonQuery("P_InsertCategory", paras, CommandType.StoredProcedure);
            id = paras[0].Value.ToString();
            return id;
        }

        public string AddProduct(string productCode, string productName, string generalName, bool iscombineproduct, string brandid, string bigunitid, string smallunitid, int bigSmallMultiple,
                                 string categoryid, int status, string attrlist, string valuelist, string attrvaluelist, decimal commonprice, decimal price,
                                 decimal weight, bool isnew, bool isRecommend, int effectiveDays, decimal discountValue, string productImg, string shapeCode, string description, string operateid, string clientid)
        {
            string id = "";
            SqlParameter[] paras = { 
                                       new SqlParameter("@ProductID",SqlDbType.NVarChar,64),
                                       new SqlParameter("@ProductCode",productCode),
                                       new SqlParameter("@ProductName",productName),
                                       new SqlParameter("@GeneralName",generalName),
                                       new SqlParameter("@IsCombineProduct",iscombineproduct),
                                       new SqlParameter("@BrandID",brandid),
                                       new SqlParameter("@BigUnitID",bigunitid),
                                       new SqlParameter("@SmallUnitID",smallunitid),
                                       new SqlParameter("@BigSmallMultiple",bigSmallMultiple),
                                       new SqlParameter("@CategoryID",categoryid),
                                       new SqlParameter("@Status",status),
                                       new SqlParameter("@AttrList",attrlist),
                                       new SqlParameter("@ValueList",valuelist),
                                       new SqlParameter("@AttrValueList",attrvaluelist),
                                       new SqlParameter("@CommonPrice",commonprice),
                                       new SqlParameter("@Price",price),
                                       new SqlParameter("@Weight",weight),
                                       new SqlParameter("@Isnew",isnew ? 1 :0),
                                       new SqlParameter("@IsRecommend",isRecommend ? 1 : 0),
                                       new SqlParameter("@EffectiveDays",effectiveDays),
                                       new SqlParameter("@DiscountValue",discountValue),
                                       new SqlParameter("@ProductImg",productImg),
                                       new SqlParameter("@ShapeCode",shapeCode),
                                       new SqlParameter("@Description",description),
                                       new SqlParameter("@CreateUserID",operateid),
                                       new SqlParameter("@ClientID",clientid)
                                   };
            paras[0].Value = id;
            paras[0].Direction = ParameterDirection.InputOutput;

            ExecuteNonQuery("P_InsertProduct", paras, CommandType.StoredProcedure);
            id = paras[0].Value.ToString();
            return id;
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

        public bool UpdateCategory(string categoryid, string categoryName, int status, string attrlist, string saleattr, string description)
        {
            string sql = "Update C_Category set CategoryName=@CategoryName,Status=@Status,AttrList=@AttrList,SaleAttr=@SaleAttr,Description=@Description,UpdateTime=getdate() where CategoryID=@CategoryID";
            SqlParameter[] paras = { 
                                       new SqlParameter("@CategoryID",categoryid),
                                       new SqlParameter("@CategoryName",categoryName),
                                       new SqlParameter("@Status",status),
                                       new SqlParameter("@AttrList",attrlist),
                                       new SqlParameter("@SaleAttr",saleattr),
                                       new SqlParameter("@Description",description)
                                   };

            return ExecuteNonQuery(sql, paras, CommandType.Text) > 0;
           
        }
        #endregion
    }
}
