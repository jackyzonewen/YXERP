using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YXERP.Controllers
{
    [YXERP.Common.UserAuthorize]
    public class BaseController : Controller
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        protected CloudSalesEntity.C_Users CurrentUser
        {
            get
            {
                if (Session["ClientManager"] == null)
                {
                    return null;
                }
                else
                {
                    return (CloudSalesEntity.C_Users)Session["ClientManager"];
                }
            }
            set { Session["ClientManager"] = value; }
        }


        protected Dictionary<string, object> JsonDictionary = new Dictionary<string, object>();

        /// <summary>
        /// 模块跳转
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult TransCenter(string id)
        {
            if (CurrentUser == null)
            {
                return Redirect("/Home/Login");
            }
            var menu = CurrentUser.Menus.Where(m => m.MenuCode == id).FirstOrDefault();
            if (menu == null)
            {
                return Redirect("/Home/Index");
            }
            HttpContext.Session["topMenuCode"] = id;
            return Redirect("/" + menu.Controller);

        }

    }
}
