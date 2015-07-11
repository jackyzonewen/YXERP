using CloudSalesBusiness;
using CloudSalesEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace YXERP.Controllers
{
    public class WarehouseController : BaseController
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
            ViewBag.Items = new WarehouseBusiness().GetWarehouseTypes(CurrentUser.ClientID);
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


        #region Ajax

        /// <summary>
        /// 保存库别
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public JsonResult SaveWareHouseType(string type)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            C_WareHouseType model = serializer.Deserialize<C_WareHouseType>(type);

            string attrID = string.Empty;
            if (string.IsNullOrEmpty(model.TypeID))
            {
                attrID = new WarehouseBusiness().AddWarehouseType(model.TypeName, model.Description, CurrentUser.UserID, CurrentUser.ClientID);
            }
            else if (new WarehouseBusiness().UpdateWarehouseType(model.TypeID, model.TypeName, model.Description, OperateIP, CurrentUser.UserID))
            {
                attrID = model.TypeID.ToString();
            }


            JsonDictionary.Add("ID", attrID);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 删除库别
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult DeleteWareHouseType(string id)
        {
            bool bl = new WarehouseBusiness().DeleteWarehouseType(id, CurrentUser.UserID, CurrentUser.ClientID);
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
