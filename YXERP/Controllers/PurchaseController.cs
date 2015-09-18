
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
             ViewBag.Type = (int)EnumOrderType.RK;
            ViewBag.Title = "采购入库";
            return View("FilterProducts");
        }

        public ActionResult MyPurchase()
        {
            return View();
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

        public ActionResult PurchaseAudit()
        {
            return View();
        }

        #region Ajax

        /// <summary>
        /// 保存采购单
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public JsonResult SubmitPurchase(string doc)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var model = serializer.Deserialize<CloudSalesEntity.StorageDoc>(doc);
            model.DocType = (int)EnumDocType.RK;

            var id = OrdersBusiness.CreateStorageDoc(model, CurrentUser.UserID, OperateIP, CurrentUser.ClientID);
            JsonDictionary.Add("ID", id);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion

    }
}
