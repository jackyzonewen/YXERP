using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CloudSalesBusiness;

namespace YXManage.Controllers
{
    [YXManage.Common.UserAuthorize]
    public class ModulesController : BaseController
    {
        //
        // GET: /Modules/

        public ActionResult Index()
        {
            ViewBag.Items = ModulesBusiness.GetModules();
            return View();
        }
    }
}
