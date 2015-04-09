using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

using CloudSalesBusiness;
using CloudSalesTool;
using CloudSalesEntity;

namespace YXManage.Controllers
{
    [YXManage.Common.UserAuthorize]
    public class ClientController : BaseController
    {
        //
        // GET: /Client/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Industry = C_IndustryBusiness.GetIndustryByClientID(ClientID);
            ViewBag.Modules = M_ModulesBusiness.GetModules();
            return View();
        }

        #region Create Ajax

        /// <summary>
        /// 添加行业
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public JsonResult CreateIndustry(string name)
        {
            string id = new C_IndustryBusiness().InsertIndustry(name, "", string.Empty, ClientID);
            JsonDictionary.Add("ID", id);
            return new JsonResult()
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 添加客户端
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public JsonResult CreateClient(string client, string loginName)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            M_Clients model = serializer.Deserialize<M_Clients>(client);
            return new JsonResult()
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion

    }
}
