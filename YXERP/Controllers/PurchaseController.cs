
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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

        #region Ajax

        public JsonResult SubmitPurchase(string doc)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var model = serializer.Deserialize<CloudSalesEntity.StorageInDoc>(doc);

            var bl = false;
            JsonDictionary.Add("Status", bl);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion

    }
}
