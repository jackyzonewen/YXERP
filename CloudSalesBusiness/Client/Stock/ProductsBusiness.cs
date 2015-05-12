using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;


using CloudSalesDAL;
using CloudSalesEntity;
using CloudSalesEnum;

namespace CloudSalesBusiness
{
    public class ProductsBusiness
    {

        #region 查询

        /// <summary>
        /// 获取品牌列表
        /// </summary>
        /// <param name="keyWords">关键词</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="clientID">客户端ID</param>
        /// <returns></returns>
        public List<C_Brand> GetBrandList(string keyWords, int pageSize, int pageIndex, ref int totalCount, ref int pageCount, string clientID)
        {
            var dal = new ProductsDAL();
            DataSet ds = dal.GetBrandList(keyWords, pageSize, pageIndex, ref totalCount, ref pageCount, clientID);

            List<C_Brand> list = new List<C_Brand>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                C_Brand model = new C_Brand();
                model.FillData(dr);
                model.City = CommonCache.Citys.Where(c => c.CityCode == model.CityCode).FirstOrDefault();
                list.Add(model);
            }
            return list;
        }
        #endregion

        #region 添加

        /// <summary>
        /// 添加品牌
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="anotherName">别称</param>
        /// <param name="icoPath"></param>
        /// <param name="countryCode">国家编码</param>
        /// <param name="cityCode">城市编码</param>
        /// <param name="status">状态</param>
        /// <param name="remark">备注</param>
        /// <param name="brandStyle">风格</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <param name="clientID">客户端ID</param>
        /// <returns></returns>
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

        #region 编辑、删除

        /// <summary>
        /// 编辑品牌状态
        /// </summary>
        /// <param name="brandID">品牌ID</param>
        /// <param name="status">状态</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <returns></returns>
        public bool UpdateBrandStatus(string brandID, StatusEnum status, string operateIP, string operateID)
        {
            return CommonBusiness.Update("C_Brand", "Status", ((int)status).ToString(), " BrandID='" + brandID + "'");
        }

        #endregion
    }
}
