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
            ViewBag.Industry = IndustryBusiness.GetIndustryByClientID(ClientID);
            ViewBag.Modules = ModulesBusiness.GetModules();
            return View();
        }

        public ActionResult Detail(string id)
        {
            ViewBag.ID = id;
            ViewBag.Industry = IndustryBusiness.GetIndustryByClientID(ClientID);
            ViewBag.Modules = ModulesBusiness.GetModules();
            return View();
        }

        public ActionResult ClientAuthorize(string id)
        {
            if (string.IsNullOrEmpty(id))
               RedirectToAction("Index", "Client");
            else
            {
                var client = ClientBusiness.GetClientDetail(id);
                if (client != null)
                {
                    ViewBag.ClientID = id;
                    ViewBag.ClientName = client.CompanyName;
                    ViewBag.UserQuantity = client.UserQuantity;
                    ViewBag.EndTime = client.EndTime!=null?client.EndTime:DateTime.Now;
                    ViewBag.AuthorizeType = client.AuthorizeType;
                }
                else
                    RedirectToAction("Index", "Client");
            }
            return View();
         }
        #region Ajax

        /// <summary>
        /// 获取客户端列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="keyWords"></param>
        /// <returns></returns>
        public JsonResult GetClients(int pageIndex, string keyWords)
        {
            int totalCount = 0, pageCount = 0;
            var list = ClientBusiness.GetClients(keyWords, PageSize, pageIndex, ref totalCount, ref pageCount);
            JsonDictionary.Add("Items", list);
            JsonDictionary.Add("TotalCount", totalCount);
            JsonDictionary.Add("PageCount", pageCount);
            return new JsonResult()
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetClientDetail(string id)
        {
            var item = ClientBusiness.GetClientDetail(id);
            JsonDictionary.Add("Item", item);
            JsonDictionary.Add("Result", 1);
            return new JsonResult()
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        /// <summary>
        /// 添加行业
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public JsonResult CreateIndustry(string name)
        {
            string id = new IndustryBusiness().InsertIndustry(name, "", string.Empty, ClientID);
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
        public JsonResult SaveClient(string client, string loginName)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Clients model = serializer.Deserialize<Clients>(client);

            int result = 0;
            if (string.IsNullOrEmpty(model.ClientID))
            {
                string clientid = ClientBusiness.InsertClient(model, loginName, loginName, CurrentUser.UserID, out result);
                JsonDictionary.Add("Result", result);
                JsonDictionary.Add("ClientID", clientid);
            }
            else
            {
                bool flag = ClientBusiness.UpdateClient(model, loginName, loginName, CurrentUser.UserID, out result);
                JsonDictionary.Add("Result", flag?1:0);
            }

            return new JsonResult()
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DeleteClient(string id)
        {
            bool flag = ClientBusiness.DeleteClient(id);
            JsonDictionary.Add("Result", flag?1:0);
            return new JsonResult()
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        /// <summary>
        /// 账号是否存在
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public JsonResult IsExistLoginName(string loginName)
        {
            bool bl = OrganizationBusiness.IsExistLoginName(loginName);
            JsonDictionary.Add("Result", bl);
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
        public JsonResult SaveClientAuthorize(string client)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Clients model = serializer.Deserialize<Clients>(client);

            bool flag = ClientBusiness.ClientAuthorize(model.ClientID,model.UserQuantity,model.Status,model.EndTime);

            JsonDictionary.Add("Result", flag?1:0);
            JsonDictionary.Add("ClientID", string.Empty);
            return new JsonResult()
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        #endregion

    }
}
