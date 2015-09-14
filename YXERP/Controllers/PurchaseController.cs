using CloudSalesEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Purchase()
        {
            ViewBag.Type = (int)EnumOrderType.RK;
            ViewBag.Title = "采购入库";
            return View("FilterProducts");
        }

    }
}
