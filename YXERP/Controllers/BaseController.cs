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
        /// 默认分页Size
        /// </summary>
        protected int PageSize = 20;

        /// <summary>
        /// 登录IP
        /// </summary>
        protected string OperateIP
        {
            get 
            {
                return string.IsNullOrEmpty(Request.Headers.Get("X-Real-IP")) ? Request.UserHostAddress : Request.Headers["X-Real-IP"];
            }
        }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        protected CloudSalesEntity.Users CurrentUser
        {
            get
            {
                if (Session["ClientManager"] == null)
                {
                    return null;
                }
                else
                {
                    return (CloudSalesEntity.Users)Session["ClientManager"];
                }
            }
            set { Session["ClientManager"] = value; }
        }

        /// <summary>
        /// 返回数据集合
        /// </summary>
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
