using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YXManage.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            if (CurrentUser == null)
            {
                return Redirect("/Home/Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            if (CurrentUser != null)
            {
                return Redirect("/Home/Index");
            }
            return View();
        }
        public ActionResult Logout()
        {
            CurrentUser = null;
            return Redirect("/Home/Login");
        }

        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public JsonResult UserLogin(string userName, string pwd)
        {
            bool bl = false;
            CloudSalesEntity.M_Users model = CloudSalesBusiness.M_UsersBusiness.GetM_UserByUserName(userName, pwd);
            if (model != null)
            {
                CurrentUser = model;
                bl = true;
            }
            Json.Add("result", bl);
            return new JsonResult
            {
                Data = Json,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

    }
}
