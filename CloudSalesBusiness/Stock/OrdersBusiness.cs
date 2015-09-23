using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CloudSalesEnum;
using CloudSalesDAL;
using CloudSalesEntity;
using System.Data;
using System.Data.SqlClient;

namespace CloudSalesBusiness
{
    public class OrdersBusiness
    {

        #region 查询

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ordertype">订单类型</param>
        /// <param name="userid">操作员</param>
        /// <returns></returns>
        public static int GetShoppingCartCount(EnumOrderType ordertype, string userid)
        {
            object obj = CommonBusiness.Select("ShoppingCart", "count(0)", "ordertype=" + (int)ordertype + " and UserID='" + userid + "'");
            return Convert.ToInt32(obj);
        }
        /// <summary>
        /// 获取购物车列表
        /// </summary>
        /// <param name="ordertype"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static List<ProductDetail> GetShoppingCart(EnumOrderType ordertype, string userid)
        {
            DataTable dt = OrdersDAL.GetShoppingCart((int)ordertype, userid);
            List<ProductDetail> list = new List<ProductDetail>();
            foreach (DataRow dr in dt.Rows)
            {
                ProductDetail model = new ProductDetail();
                model.FillData(dr);
                list.Add(model);
            }
            return list;
        }

        /// <summary>
        /// 获取单据列表
        /// </summary>
        /// <param name="userid">创建人（拥有者）</param>
        /// <param name="type">类型</param>
        /// <param name="status">状态</param>
        /// <param name="keywords">关键词</param>
        /// <param name="pageSize">页Size</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="totalCount">总数</param>
        /// <param name="pageCount"><总页/param>
        /// <param name="clientID">客户端ID</param>
        /// <returns></returns>
        public static List<StorageDoc> GetStorageDocList(string userid, EnumDocType type, EnumDocStatus status, string keywords, int pageSize, int pageIndex, ref int totalCount, ref int pageCount, string clientID)
        {
            DataSet ds = OrdersDAL.GetStorageDocList(userid, (int)type, (int)status, keywords, pageSize, pageIndex, ref totalCount, ref pageCount, clientID);

            List<StorageDoc> list = new List<StorageDoc>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                StorageDoc model = new StorageDoc();
                model.FillData(dr);
                model.CreateUser = OrganizationBusiness.GetUserByUserID(model.CreateUserID, clientID);
                model.StatusStr = GetDocStatusStr(model.DocType, model.Status);

                list.Add(model);
            }
            return list;
        }
        
        /// <summary>
        /// 获取单据详情
        /// </summary>
        /// <param name="docid">单据ID</param>
        /// <param name="clientid">客户端ID</param>
        /// <returns></returns>
        public static StorageDoc GetStorageDetail(string docid, string clientid)
        {
            DataSet ds = OrdersDAL.GetStorageDetail(docid, clientid);
            StorageDoc model = new StorageDoc();
            if (ds.Tables.Contains("Doc") && ds.Tables["Doc"].Rows.Count > 0)
            {
                model.FillData(ds.Tables["Doc"].Rows[0]);
                model.CreateUser = OrganizationBusiness.GetUserByUserID(model.CreateUserID, clientid);
                model.StatusStr = GetDocStatusStr(model.DocType, model.Status);

                model.Details = new List<StorageDetail>();
                foreach (DataRow item in ds.Tables["Details"].Rows)
                {
                    StorageDetail details = new StorageDetail();
                    details.FillData(item);
                    model.Details.Add(details);
                }
            }

            return model;
        }
        /// <summary>
        /// 单据状态
        /// </summary>
        /// <param name="doctype">类型</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        private static string GetDocStatusStr(int doctype, int status)
        {
            string str = "";
            switch (status)
            {
                case 0:
                    str = "待审核";
                    break;
                case 1:
                    str = doctype == 1 ? "部分上架" 
                        : doctype == 2 ? "部分出库" 
                        : "部分审核";
                    break;
                case 2:
                    str = doctype == 1 ? "已上架"
                        : doctype == 2 ? "已出库"
                        : "已审核";
                    break;
                case 4:
                    str = "已作废";
                    break;
                case 9:
                    str = "已删除";
                    break;
            }
            return str;
        }

        #endregion

        #region 添加

        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <param name="productid">产品ID</param>
        /// <param name="detailsid">产品详情ID</param>
        /// <param name="quantity">数量</param>
        /// <param name="unitid">单位</param>
        /// <param name="isBigUnit">是否大单位</param>
        /// <param name="ordertype">订单类型</param>
        /// <param name="remark">备注</param>
        /// <param name="userid">操作员</param>
        /// <param name="operateip">操作IP</param>
        /// <returns></returns>
        public static bool AddShoppingCart(string productid, string detailsid, int quantity, string unitid, int isBigUnit, EnumOrderType ordertype, string remark, string userid, string operateip)
        {
            return OrdersDAL.AddShoppingCart(productid, detailsid, quantity, unitid, isBigUnit, (int)ordertype, remark, userid, operateip);
        }

        /// <summary>
        /// 创建单据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string CreateStorageDoc(StorageDoc model, string userid, string operateip, string clientid)
        {
            string docid = Guid.NewGuid().ToString();
            SqlConnection conn = new SqlConnection(OrdersDAL.ConnectionString);
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();

            try
            {

                bool bl = OrdersDAL.AddStorageDoc(docid, model.DocType, model.TotalMoney, model.CityCode, model.Address, model.Remark, userid, operateip, clientid, tran);
                if (bl)
                {
                    //单据明细
                    foreach (var detail in model.Details)
                    {
                        if (!OrdersDAL.AddStorageDocDetail(docid, detail.AutoID, detail.ProductDetailID, detail.Quantity, detail.Price, detail.TotalMoney, detail.BatchCode, clientid, tran))
                        {
                            tran.Rollback();
                            conn.Dispose();
                            return "";
                        }
                    }
                    tran.Commit();
                    conn.Dispose();
                    return docid;
                }
                else
                {
                    tran.Rollback();
                    conn.Dispose();
                    return "";
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                conn.Dispose();
                return "";
            }            
        }

        #endregion

        #region 编辑、删除

        /// <summary>
        /// 编辑购物车产品数量
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public static bool UpdateCartQuantity(string autoid, int quantity, string userid, string clientid)
        {
            return CommonBusiness.Update("ShoppingCart", "Quantity", quantity, "AutoID=" + autoid);
        }

        /// <summary>
        /// 删除购物车记录
        /// </summary>
        /// <param name="autoid"></param>
        /// <param name="userid"></param>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public static bool DeleteCart(string autoid, string userid, string clientid)
        {
            return CommonBusiness.Delete("ShoppingCart", "AutoID=" + autoid);
        }
        /// <summary>
        /// 删除单据
        /// </summary>
        /// <param name="docid">单据ID</param>
        /// <param name="userid">操作人</param>
        /// <param name="operateip">操作IP</param>
        /// <param name="clientid">客户端ID</param>
        /// <returns></returns>
        public bool DeleteDoc(string docid, string userid, string operateip, string clientid)
        {
            return CommonBusiness.Update("StorageDoc", "Status", (int)EnumDocStatus.Delete, "DocID='" + docid + "' and status=" + (int)EnumDocStatus.Normal);
        }
        /// <summary>
        /// 作废单据
        /// </summary>
        /// <param name="docid"></param>
        /// <param name="userid"></param>
        /// <param name="operateip"></param>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public bool InvalidDoc(string docid, string userid, string operateip, string clientid)
        {
            return CommonBusiness.Update("StorageDoc", "Status", (int)EnumDocStatus.Invalid, "DocID='" + docid + "' and status=" + (int)EnumDocStatus.Normal);
        }
        /// <summary>
        /// 更换入库仓库
        /// </summary>
        /// <param name="autoid">单据详情ID</param>
        /// <param name="wareid">仓库ID</param>
        /// <param name="depotid">货位ID</param>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public bool UpdateStorageDetailWare(string autoid, string wareid, string depotid, string userid, string operateip, string clientid)
        {
            return new OrdersDAL().UpdateStorageDetailWare(autoid, wareid, depotid);
        }

        #endregion
    }
}
