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


        #region 查询

        public static DataTable GetShoppingCart(int ordertype, string userid)
        {
            SqlParameter[] paras = { 
                                     new SqlParameter("@OrderType",ordertype),
                                     new SqlParameter("@UserID" , userid),
                                   };
            return GetDataTable("P_GetShoppingCart", paras, CommandType.StoredProcedure);
        }

        #endregion
    }
}
