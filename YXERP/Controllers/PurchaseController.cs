
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CloudSalesEnum;
using CloudSalesBusiness;

namespace YXERP.Controllers
{
    public class PurchaseController : BaseController
    {
        //
        // GET: /Purchase/

        public ActionResult Index()
        {
            return View("Purchase");
        }
        /// <summary>
        /// 我的采购
        /// </summary>
        /// <returns></returns>
        public ActionResult Purchase()
        {
            return View();
        }

        public ActionResult CreatePurchase()
        {
            ViewBag.Type = (int)EnumOrderType.RK;
            ViewBag.Title = "采购入库";
            return View("FilterProducts");
        }

        /// <summary>
        /// 采购单确认页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ConfirmPurchase()
        {
            ViewBag.Items = OrdersBusiness.GetShoppingCart(EnumOrderType.RK, CurrentUser.UserID);
            return View();
        }

    }
}
