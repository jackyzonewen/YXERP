using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YXManage1._0.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            if (Session["Manager"] == null)
            {
                return Redirect("/Home/Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            if (Session["Manager"] != null)
            {
                return Redirect("/Home/Index");
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["Manager"] = null;
            return Redirect("/Home/Login");
        }


        public JsonResult UserLogin(string userName, string pwd)
        {
            pwd = CloudSalesTool.Encrypt.GetEncryptPwd(pwd, userName);
            return new JsonResult() 
            { 
                Data = "", 
                JsonRequestBehavior = JsonRequestBehavior.AllowGet 
            };
        }

    }
}
