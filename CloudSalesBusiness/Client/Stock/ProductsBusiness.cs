using CloudSalesDAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace CloudSalesBusiness
{
    public class ProductsBusiness
    {
        #region 添加

        /// <summary>
        /// 添加品牌
        /// </summary>
        /// <returns>返回品牌ID</returns>
        public string AddBrand(string name, string anotherName, string icoPath, string countryCode, string cityCode, int status, string remark, string brandStyle, string operateIP, string operateID, string clientID)
        {
            if (!string.IsNullOrEmpty(icoPath) && icoPath != "/modules/images/default.png")
            {
                if (icoPath.IndexOf("?") > 0)
                {
                    icoPath = icoPath.Substring(0, icoPath.IndexOf("?"));
                }
                FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(icoPath));
                icoPath = "/Content/uploadFiles/" + file.Name;
                if (file.Exists)
                {
                    file.MoveTo(HttpContext.Current.Server.MapPath(icoPath));
                }
            }
            return new ProductsDAL().AddBrand(name, anotherName, icoPath, countryCode, cityCode, status, remark, brandStyle, operateIP, operateID, clientID);
        }

        #endregion
    }
}
