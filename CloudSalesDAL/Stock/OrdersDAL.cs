using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSalesDAL
{
    public class OrdersDAL : BaseDAL
    {

        #region 查询

        public static DataTable GetShoppingCart(int ordertype, string userid)
        {
            SqlParameter[] paras = { 
                                     new SqlParameter("@OrderType",ordertype),
                                     new SqlParameter("@UserID" , userid),
                                   };
            return GetDataTable("P_GetShoppingCart", paras, CommandType.StoredProcedure);
        }

        public static DataSet GetStorageDocList(string userid, int type, int status, string keywords, int pageSize, int pageIndex, ref int totalCount, ref int pageCount, string clientid)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@TotalCount",SqlDbType.Int),
                                       new SqlParameter("@PageCount",SqlDbType.Int),
                                       new SqlParameter("@UserID", userid),
                                       new SqlParameter("@DocType", type),
                                       new SqlParameter("@Status", status),
                                       new SqlParameter("@KeyWords",keywords),
                                       new SqlParameter("@PageSize",pageSize),
                                       new SqlParameter("@PageIndex",pageIndex),
                                       new SqlParameter("@ClientID",clientid)
                                   };
            paras[0].Value = totalCount;
            paras[1].Value = pageCount;

            paras[0].Direction = ParameterDirection.InputOutput;
            paras[1].Direction = ParameterDirection.InputOutput;
            DataSet ds = GetDataSet("P_GetStorageDocList", paras, CommandType.StoredProcedure, "Doc");
            totalCount = Convert.ToInt32(paras[0].Value);
            pageCount = Convert.ToInt32(paras[1].Value);
            return ds;
        }

        public static DataSet GetStorageDetail(string docid, string clientid)
        {
            SqlParameter[] paras = { 
                                       new SqlParameter("@DocID",docid),
                                       new SqlParameter("@ClientID",clientid)
                                   };

            DataSet ds = GetDataSet("P_GetStorageDetail", paras, CommandType.StoredProcedure, "Doc|Details");
            return ds;
        }

        #endregion

        #region 添加

        public static bool AddShoppingCart(string productid, string detailsid, int quantity, string unitid, int isBigUnit, int ordertype, string remark, string userid, string operateip)
        {
            SqlParameter[] paras = { 
                                     new SqlParameter("@OrderType",ordertype),
                                     new SqlParameter("@ProductDetailID",detailsid),
                                     new SqlParameter("@ProductID" , productid),
                                     new SqlParameter("@Quantity" , quantity),
                                     new SqlParameter("@UnitID" , unitid),
                                     new SqlParameter("@IsBigUnit" , isBigUnit),
                                     new SqlParameter("@Remark" , remark),
                                     new SqlParameter("@UserID" , userid),
                                     new SqlParameter("@OperateIP" , operateip)
                                   };
            return ExecuteNonQuery("P_AddShoppingCart", paras, CommandType.StoredProcedure) > 0;
        }

        public static bool AddStorageDoc(string docid, int doctype, decimal totalmoney, string cityCode, string address, string remark, string userid, string operateip, string clientid, SqlTransaction tran)
        {
            SqlParameter[] paras = { 
                                     new SqlParameter("@DocID",docid),
                                     new SqlParameter("@DocCode",DateTime.Now.ToString("yyyyMMddHHmmssfff")),
                                     new SqlParameter("@DocType",doctype),
                                     new SqlParameter("@TotalMoney" , totalmoney),
                                     new SqlParameter("@CityCode" , cityCode),
                                     new SqlParameter("@Address" , address),
                                     new SqlParameter("@Remark" , remark),
                                     new SqlParameter("@UserID" , userid),
                                     new SqlParameter("@OperateIP" , operateip),
                                     new SqlParameter("@ClientID" , clientid)
                                   };
            return ExecuteNonQuery(tran, "P_AddStorageDoc", paras, CommandType.StoredProcedure) > 0;
        }

        public static bool AddStorageDocDetail(string docid, int cartAutoID, string productdetailid, int qunatity, decimal price, decimal totalmoney, string batchcode, string clientid, SqlTransaction tran)
        {
            SqlParameter[] paras = { 
                                     new SqlParameter("@DocID",docid),
                                     new SqlParameter("@AutoID",cartAutoID),
                                     new SqlParameter("@ProductDetailID" , productdetailid),
                                     new SqlParameter("@Quantity" , qunatity),
                                     new SqlParameter("@Price" , price),
                                     new SqlParameter("@TotalMoney" , totalmoney),
                                     new SqlParameter("@BatchCode" , batchcode),
                                     new SqlParameter("@ClientID" , clientid)
                                   };


            return ExecuteNonQuery(tran, "P_AddStorageDetail", paras, CommandType.StoredProcedure) > 0;

        }

        #endregion

        #region 编辑、删除


        #endregion

    }
}
