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

        public ActionResult WareHouseAdd()
        {
            return View();
        }

        public ActionResult WareHouseDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View("WareHouse");
            }
            C_WareHouse model = new WarehouseBusiness().GetWareByID(id);
            ViewBag.Item = model;
            ViewBag.ID = id;

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

            string typeID = string.Empty;
            if (string.IsNullOrEmpty(model.TypeID))
            {
                typeID = new WarehouseBusiness().AddWarehouseType(model.TypeName, model.Description, CurrentUser.UserID, CurrentUser.ClientID);
            }
            else if (new WarehouseBusiness().UpdateWarehouseType(model.TypeID, model.TypeName, model.Description, OperateIP, CurrentUser.UserID))
            {
                typeID = model.TypeID;
            }


            JsonDictionary.Add("ID", typeID);
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

        /// <summary>
        /// 获取仓库列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetWareHouses(string keyWords, int pageSize, int pageIndex, int totalCount)
        {
            int pageCount = 0;
            List<C_WareHouse> list = new WarehouseBusiness().GetWareHouses(keyWords, pageSize, pageIndex, ref totalCount, ref pageCount, CurrentUser.ClientID);
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
        /// 保存仓库
        /// </summary>
        /// <param name="ware"></param>
        /// <returns></returns>
        public JsonResult SaveWareHouse(string ware)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            C_WareHouse model = serializer.Deserialize<C_WareHouse>(ware);

            string id = string.Empty;
            if (string.IsNullOrEmpty(model.WareID))
            {
                id = new WarehouseBusiness().AddWareHouse(model.WareCode, model.Name, model.ShortName, model.CityCode, model.Status.Value, model.Description, CurrentUser.UserID, CurrentUser.ClientID);
            }
            else if (new WarehouseBusiness().UpdateWareHouse(model.WareID, model.Name, model.ShortName, model.CityCode, model.Status.Value, model.Description, CurrentUser.UserID, CurrentUser.ClientID))
            {
                id = model.WareID;
            }


            JsonDictionary.Add("ID", id);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 编辑仓库状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public JsonResult UpdateWareHouseStatus(string id, int status)
        {
            bool bl = new WarehouseBusiness().UpdateWareHouseStatus(id, (CloudSalesEnum.StatusEnum)status, CurrentUser.UserID, CurrentUser.ClientID);
            JsonDictionary.Add("Status", bl);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        /// <summary>
        /// 删除仓库
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult DeleteWareHouse(string id)
        {
            bool bl = new WarehouseBusiness().UpdateWareHouseStatus(id, CloudSalesEnum.StatusEnum.Delete, CurrentUser.UserID, CurrentUser.ClientID);
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
