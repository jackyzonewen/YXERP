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

        public DataTable GetUnitByUnitID(string unitID) 
        {
            SqlParameter[] paras = { new SqlParameter("@unitID", unitID) };
            DataTable dt = GetDataTable("select * from C_ProductUnit where UnitID=@unitID", paras, CommandType.Text);
            return dt;
        }

        public DataTable GetClientUnits(string clientid)
        {
            SqlParameter[] paras = { new SqlParameter("@ClientID", clientid) };
            DataTable dt = GetDataTable("select * from C_ProductUnit where ClientID=@ClientID and Status<>9", paras, CommandType.Text);
            return dt;
        }

        public DataSet GetAttrList(string categoryid, string keyWords, int pageSize, int pageIndex, ref int totalCount, ref int pageCount, string clientid)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@totalCount",SqlDbType.Int),
                                       new SqlParameter("@pageCount",SqlDbType.Int),
                                       new SqlParameter("@keyWords",keyWords),
                                       new SqlParameter("@pageSize",pageSize),
                                       new SqlParameter("@pageIndex",pageIndex),
                                       new SqlParameter("@CategoryID", categoryid),
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

        public DataTable GetAttrList(string categoryid, string clientid)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@CategoryID", categoryid),
                                       new SqlParameter("@ClientID", clientid) 
                                   };

            return GetDataTable("P_GetAttrsByCategoryID", paras, CommandType.StoredProcedure);

        }

        public DataSet GetProductAttrByID(string attrID)
        {
            SqlParameter[] paras = { new SqlParameter("@AttrID", attrID) };
            DataSet ds = GetDataSet("P_GetProductAttrByID", paras, CommandType.StoredProcedure, "Attrs|Values");
            return ds;
        }

        public DataTable GetChildCategorysByID(string categoryid, string clientid)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@PID", categoryid) ,
                                       new SqlParameter("@ClientID", clientid) 
                                   };
            DataTable dt = GetDataTable("select * from C_Category where PID=@PID and ClientID=@ClientID Order by CreateTime", paras, CommandType.Text);
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

        public DataSet GetProductByID(string productid)
        {
            SqlParameter[] paras = { new SqlParameter("@ProductID", productid) };
            DataSet ds = GetDataSet("P_GetProductByID", paras, CommandType.StoredProcedure, "Product|Details|Unit");
            return ds;
        }

        public DataSet GetFilterProducts(string categoryid, string attrwhere, string salewhere, string beginprice, string endprice, string keyWords, string orderby, int isasc, int pageSize, int pageIndex, ref int totalCount, ref int pageCount, string clientID)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@totalCount",SqlDbType.Int),
                                       new SqlParameter("@pageCount",SqlDbType.Int),
                                       new SqlParameter("@orderColumn",orderby),
                                       new SqlParameter("@isAsc",isasc),
                                       new SqlParameter("@AttrWhere",attrwhere),
                                       new SqlParameter("@SaleWhere",salewhere),
                                       new SqlParameter("@BeginPrice",beginprice),
                                       new SqlParameter("@EndPrice",endprice),
                                       new SqlParameter("@CategoryID",categoryid),
                                       new SqlParameter("@keyWords",keyWords),
                                       new SqlParameter("@pageSize",pageSize),
                                       new SqlParameter("@pageIndex",pageIndex),
                                       new SqlParameter("@ClientID",clientID)
                                       
                                   };
            paras[0].Value = totalCount;
            paras[1].Value = pageCount;

            paras[0].Direction = ParameterDirection.InputOutput;
            paras[1].Direction = ParameterDirection.InputOutput;
            DataSet ds = GetDataSet("P_GetFilterProducts", paras, CommandType.StoredProcedure);
            totalCount = Convert.ToInt32(paras[0].Value);
            pageCount = Convert.ToInt32(paras[1].Value);
            return ds;

        }

        public DataSet GetProductByIDForDetails(string productid)
        {
            SqlParameter[] paras = { new SqlParameter("@ProductID", productid) };
            DataSet ds = GetDataSet("P_GetProductByIDForDetails", paras, CommandType.StoredProcedure, "Product|Details|Unit|Attrs|Values");
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

        public bool AddProductAttr(string attrID, string attrName, string description, string categoryID, int type, string operateid, string clientid)
        {
            string sqlText = "P_InsertAttr";
            SqlParameter[] paras = { 
                                     new SqlParameter("@AttrID" , attrID),
                                     new SqlParameter("@AttrName" , attrName),
                                     new SqlParameter("@Description" , description),
                                     new SqlParameter("@CategoryID" , categoryID),
                                     new SqlParameter("@Type" , type),
                                     new SqlParameter("@CreateUserID" , operateid),
                                     new SqlParameter("@ClientID" , clientid)
                                   };
            return ExecuteNonQuery(sqlText, paras, CommandType.StoredProcedure) > 0;
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
            int result = 0;
            SqlParameter[] paras = { 
                                       new SqlParameter("@ProductID",SqlDbType.NVarChar,64),
                                       new SqlParameter("@Result",SqlDbType.Int),
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
            paras[1].Value = result;
            paras[1].Direction = ParameterDirection.InputOutput;

            ExecuteNonQuery("P_InsertProduct", paras, CommandType.StoredProcedure);
            id = paras[0].Value.ToString();
            result = Convert.ToInt32(paras[1].Value);
            return id;
        }

        public bool AddCategoryAttr(string categoryid, string attrid, int type, string operateid)
        {
            SqlParameter[] paras = { 
                                     new SqlParameter("@CategoryID",categoryid),
                                     new SqlParameter("@AttrID",attrid),
                                     new SqlParameter("@Type" , type),
                                     new SqlParameter("@CreateUserID" , operateid)
                                   };
            return ExecuteNonQuery("P_AddCategoryAttr", paras, CommandType.StoredProcedure) > 0;
        }

        public string AddProductDetails(string productid, string productCode, string shapeCode, string attrlist, string valuelist, string attrvaluelist, decimal price, decimal weight, string unitid, string productImg, string description, string operateid, string clientid)
        {
            string id = "";
            int result = 0;
            SqlParameter[] paras = { 
                                       new SqlParameter("@DetailID",SqlDbType.NVarChar,64),
                                       new SqlParameter("@Result",SqlDbType.Int),
                                       new SqlParameter("@ProductCode",productCode),
                                       new SqlParameter("@UnitID",unitid),
                                       new SqlParameter("@ProductID",productid),
                                       new SqlParameter("@AttrList",attrlist),
                                       new SqlParameter("@ValueList",valuelist),
                                       new SqlParameter("@AttrValueList",attrvaluelist),
                                       new SqlParameter("@Price",price),
                                       new SqlParameter("@Weight",weight),
                                       new SqlParameter("@ProductImg",productImg),
                                       new SqlParameter("@ShapeCode",shapeCode),
                                       new SqlParameter("@Description",description),
                                       new SqlParameter("@CreateUserID",operateid),
                                       new SqlParameter("@ClientID",clientid)
                                   };
            paras[0].Value = id;
            paras[0].Direction = ParameterDirection.InputOutput;
            paras[1].Value = result;
            paras[1].Direction = ParameterDirection.InputOutput;

            ExecuteNonQuery("P_InsertProductDetail", paras, CommandType.StoredProcedure);
            id = paras[0].Value.ToString();
            result = Convert.ToInt32(paras[1].Value);
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

        public bool UpdateCategoryAttrStatus(string categoryid, string attrid, int status, int type)
        {
            string sqlText = "Update C_CategoryAttr set Status=@Status,UpdateTime=getdate()  where [AttrID]=@AttrID and CategoryID=@CategoryID and Type=@Type";
            SqlParameter[] paras = { 
                                     new SqlParameter("@CategoryID",categoryid),
                                     new SqlParameter("@AttrID",attrid),
                                     new SqlParameter("@Status" , status),
                                     new SqlParameter("@Type" , type)
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

        public bool UpdateCategory(string categoryid, string categoryName, int status, string attrlist, string saleattr, string description, string operateid)
        {
            string sql = "P_UpdateCategory";
            SqlParameter[] paras = { 
                                       new SqlParameter("@CategoryID",categoryid),
                                       new SqlParameter("@CategoryName",categoryName),
                                       new SqlParameter("@Status",status),
                                       new SqlParameter("@AttrList",attrlist),
                                       new SqlParameter("@SaleAttr",saleattr),
                                       new SqlParameter("@UserID",operateid),
                                       new SqlParameter("@Description",description)
                                   };

            return ExecuteNonQuery(sql, paras, CommandType.StoredProcedure) > 0;
           
        }

        public bool UpdateProduct(string productid, string productCode, string productName, string generalName, bool iscombineproduct, string brandid, string bigunitid, string smallunitid, int bigSmallMultiple,
                                 int status, string categoryid, string attrlist, string valuelist, string attrvaluelist, decimal commonprice, decimal price,
                                 decimal weight, bool isnew, bool isRecommend, int effectiveDays, decimal discountValue, string shapeCode, string description, string operateid, string clientid)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@ProductID",productid),
                                       new SqlParameter("@ProductCode",productCode),
                                       new SqlParameter("@ProductName",productName),
                                       new SqlParameter("@GeneralName",generalName),
                                       new SqlParameter("@IsCombineProduct",iscombineproduct),
                                       new SqlParameter("@BrandID",brandid),
                                       new SqlParameter("@BigUnitID",bigunitid),
                                       new SqlParameter("@SmallUnitID",smallunitid),
                                       new SqlParameter("@BigSmallMultiple",bigSmallMultiple),
                                       new SqlParameter("@Status",status),
                                       new SqlParameter("@CategoryID",categoryid),
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
                                       new SqlParameter("@ShapeCode",shapeCode),
                                       new SqlParameter("@Description",description),
                                       new SqlParameter("@CreateUserID",operateid),
                                       new SqlParameter("@ClientID",clientid)
                                   };

            return ExecuteNonQuery("P_UpdateProduct", paras, CommandType.StoredProcedure) > 0;

        }

        public bool UpdateProductDetails(string detailid, string productid, string productCode, string shapeCode, string unitid, string attrlist, string valuelist, string attrvaluelist, decimal price, decimal weight, string description, string image)
        {
            int result = 0;
            SqlParameter[] paras = { 
                                       new SqlParameter("@Result",SqlDbType.Int),
                                       new SqlParameter("@DetailID",detailid),
                                       new SqlParameter("@ProductID",productid),
                                       new SqlParameter("@UnitID",unitid),
                                       new SqlParameter("@ProductCode",productCode),
                                       new SqlParameter("@AttrList",attrlist),
                                       new SqlParameter("@ValueList",valuelist),
                                       new SqlParameter("@AttrValueList",attrvaluelist),
                                       new SqlParameter("@Price",price),
                                       new SqlParameter("@Weight",weight),
                                       new SqlParameter("@ShapeCode",shapeCode),
                                       new SqlParameter("@ImgS",image),
                                       new SqlParameter("@Description",description)
                                   };
            paras[0].Value = result;
            paras[0].Direction = ParameterDirection.InputOutput;


            ExecuteNonQuery("P_UpdateProductDetail", paras, CommandType.StoredProcedure);
            result = Convert.ToInt32(paras[0].Value);
            return result == 1;
        }

        #endregion
    }
}
