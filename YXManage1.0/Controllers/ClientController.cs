using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CloudSalesBusiness;

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
            ViewBag.Modules = M_ModulesBusiness.GetModules();
            return View();
        }

        #region Create Ajax

        /// <summary>
        /// 添加客户端
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public JsonResult CreateClient(string client)
        {
            return new JsonResult()
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion

    }
}
