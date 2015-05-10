using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CloudSalesBusiness;

namespace YXERP.Controllers
{
    public class PlugController : BaseController
    {
        /// <summary>
        /// 根据cityCode获取下级地区列表
        /// </summary>
        /// <param name="pcode"></param>
        /// <returns></returns>
        public JsonResult GetCityByPCode(string cityCode)
        {
            var list = CommonBusiness.GetCitys().Where(c => c.PCode == cityCode);
            JsonDictionary.Add("Items", list);
            return new JsonResult()
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

    }
}
