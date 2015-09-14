using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using CloudSalesEntity;
using YXERP.Models;
using CloudSalesBusiness;
using CloudSalesEnum;

namespace YXERP.Controllers
{
    public class OrdersController : BaseController
    {
        //
        // GET: /Orders/

        public ActionResult Index()
        {
            return View();
        }

        #region Ajax

        /// <summary>
        /// 过滤产品
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public JsonResult GetProductListForShopping(string filter)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            FilterProduct model = serializer.Deserialize<FilterProduct>(filter);
            int totalCount = 0;
            int pageCount = 0;

            List<C_Products> list = new ProductsBusiness().GetFilterProducts(model.CategoryID, model.Attrs, model.BeginPrice, model.EndPrice, model.Keywords, model.OrderBy, model.IsAsc, 20, model.PageIndex, ref totalCount, ref pageCount, CurrentUser.ClientID);
            JsonDictionary.Add("Items", list);
            JsonDictionary.Add("TotalCount", totalCount);
            JsonDictionary.Add("PageCount", pageCount);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 加入到购物车
        /// </summary>
        /// <param name="productid"></param>
        /// <param name="detailsid"></param>
        /// <param name="quantity"></param>
        /// <param name="ordertype"></param>
        /// <returns></returns>
        public JsonResult AddShoppingCart(string productid, string detailsid, int quantity, EnumOrderType ordertype, string remark)
        {
            var bl = OrdersBusiness.AddShoppingCart(productid, detailsid, quantity, ordertype, remark, CurrentUser.UserID, OperateIP);
            JsonDictionary.Add("Status", bl);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        /// <summary>
        /// 获取购物车产品数
        /// </summary>
        /// <param name="ordertype"></param>
        /// <returns></returns>
        public JsonResult GetShoppingCartCount(EnumOrderType ordertype)
        {
            var count = OrdersBusiness.GetShoppingCartCount(ordertype, CurrentUser.UserID);
            JsonDictionary.Add("Quantity", count);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }



        #endregion

    }
}
