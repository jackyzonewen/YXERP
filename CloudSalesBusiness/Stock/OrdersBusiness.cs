using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CloudSalesEnum;
using CloudSalesDAL;
using CloudSalesEntity;
using System.Data;

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

        #endregion

        #region 编辑

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

        #endregion
    }
}
