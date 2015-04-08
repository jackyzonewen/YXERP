using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YXManage.Controllers
{
    public class BaseController : Controller
    {
        protected Dictionary<string, object> JsonDictionary = new Dictionary<string, object>();

        protected CloudSalesEntity.M_Users CurrentUser
        {
            get
            {
                if (Session["Manager"] == null)
                {
                    return null;
                }
                else
                {
                    return (CloudSalesEntity.M_Users)Session["Manager"];
                }
            }
            set { Session["Manager"] = value; }
        }

    }
}
