using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YXERP.Controllers
{
    public class WarehouseController : Controller
    {
        //
        // GET: /Warehouse/

        public ActionResult Index()
        {
            return View("WareHouse");
        }

        /// <summary>
        /// 库别设置
        /// </summary>
        /// <returns></returns>
        public ActionResult WareType()
        {
            return View();
        }
        /// <summary>
        /// 仓库设置
        /// </summary>
        /// <returns></returns>
        public ActionResult WareHouse()
        {
            return View();
        }
        /// <summary>
        /// 货位设置
        /// </summary>
        /// <returns></returns>
        public ActionResult DepotSeat()
        {
            return View();
        }
    }
}
