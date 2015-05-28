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

        /// <summary>
        /// 获取品牌实体
        /// </summary>
        /// <param name="brandID">传入参数</param>
        /// <returns></returns>
        public C_Brand GetBrandByBrandID(string brandID)
        {
            var dal = new ProductsDAL();
            DataTable dt = dal.GetBrandByBrandID(brandID);

            C_Brand model = new C_Brand();
            if (dt.Rows.Count > 0)
            {
                model.FillData(dt.Rows[0]);
                model.City = CommonCache.Citys.Where(c => c.CityCode == model.CityCode).FirstOrDefault();
            }
            return model;
        }

        /// <summary>
        /// 获取单位列表
        /// </summary>
        /// <returns></returns>
        public List<C_ProductUnit> GetClientUnits(string clientid)
        {
            var dal = new ProductsDAL();
            DataTable dt = dal.GetClientUnits(clientid);

            List<C_ProductUnit> list = new List<C_ProductUnit>();
            foreach (DataRow dr in dt.Rows)
            {
                C_ProductUnit model = new C_ProductUnit();
                model.FillData(dr);
                list.Add(model);
            }
            return list;
        }

        /// <summary>
        /// 获取属性列表
        /// </summary>
        /// <param name="keyWords"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="totalCount"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        public List<C_ProductAttr> GetAttrList(string keyWords, int pageSize, int pageIndex, ref int totalCount, ref int pageCount, string clientid)
        {
            var dal = new ProductsDAL();
            DataSet ds = dal.GetAttrList(keyWords, pageSize, pageIndex, ref totalCount, ref pageCount, clientid);

            List<C_ProductAttr> list = new List<C_ProductAttr>();
            if (ds.Tables.Contains("Attrs"))
            {
                foreach (DataRow dr in ds.Tables["Attrs"].Rows)
                {
                    C_ProductAttr model = new C_ProductAttr();
                    model.FillData(dr);

                    List<C_AttrValue> valueList = new List<C_AttrValue>();
                    StringBuilder build = new StringBuilder();
                    foreach (DataRow drValue in ds.Tables["Values"].Select("AttrID='" + model.AttrID + "'"))
                    {
                        C_AttrValue valueModel = new C_AttrValue();
                        valueModel.FillData(drValue);
                        valueList.Add(valueModel);
                        build.Append(valueModel.ValueName + ",");
                    }
                    model.AttrValues = valueList;
                    if (string.IsNullOrEmpty(build.ToString()))
                    {
                        model.ValuesStr = "暂无属性值(单击添加)";
                    }
                    else
                    {
                        if (build.ToString().Length > 50)
                        {
                            if (build.ToString().Substring(49, 1).ToString() != ",")
                            {
                                model.ValuesStr = build.ToString() + "...";
                            }
                            else
                            {
                                model.ValuesStr = build.ToString().Substring(0, 49) + " ...";
                            }
                        }
                        else
                        {
                            model.ValuesStr = build.ToString().Substring(0, build.ToString().Length - 1);
                        }
                    }
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取属性列表
        /// </summary>
        /// <returns></returns>
        public List<C_ProductAttr> GetAttrList(string clientid)
        {
            var dal = new ProductsDAL();
            DataTable dt = dal.GetAttrList(clientid);

            List<C_ProductAttr> list = new List<C_ProductAttr>();
            foreach (DataRow dr in dt.Rows)
            {
                C_ProductAttr model = new C_ProductAttr();
                model.FillData(dr);
                list.Add(model);
            }
            return list;
        }


        /// <summary>
        /// 根据属性ID获取属性
        /// </summary>
        /// <param name="attrID"></param>
        /// <returns></returns>
        public C_ProductAttr GetProductAttrByID(string attrID)
        {
            var dal = new ProductsDAL();
            DataSet ds = dal.GetProductAttrByID(attrID);

            C_ProductAttr model = new C_ProductAttr();
            if (ds.Tables.Contains("Attrs") && ds.Tables["Attrs"].Rows.Count > 0)
            {
                model.FillData(ds.Tables["Attrs"].Rows[0]);
                List<C_AttrValue> list = new List<C_AttrValue>();
                foreach (DataRow item in ds.Tables["Values"].Rows)
                {
                    C_AttrValue attrValue = new C_AttrValue();
                    attrValue.FillData(item);
                    list.Add(attrValue);
                }
                model.AttrValues = list;
            }
            
            
            return model;
        }

        /// <summary>
        /// 获取下级分类
        /// </summary>
        /// <param name="categoryid">分类ID</param>
        /// <returns></returns>
        public List<C_Category> GetChildCategorysByID(string categoryid)
        {
            var dal = new ProductsDAL();
            DataTable dt = dal.GetChildCategorysByID(categoryid);

            List<C_Category> list = new List<C_Category>();

            foreach (DataRow dr in dt.Rows)
            {
                C_Category model = new C_Category();
                model.FillData(dr);
                list.Add(model);
            }


            return list;
        }

        /// <summary>
        /// 获取产品分类
        /// </summary>
        /// <param name="categoryid">分类ID</param>
        /// <returns></returns>
        public C_Category GetCategoryByID(string categoryid)
        {
            var dal = new ProductsDAL();
            DataTable dt = dal.GetCategoryByID(categoryid);

            C_Category model = new C_Category();
            if (dt.Rows.Count > 0)
            {
                model.FillData(dt.Rows[0]);
            }

            return model;
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

        /// <summary>
        /// 添加单位
        /// </summary>
        /// <param name="unitName">单位名称</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public string AddUnit(string unitName, string description,string operateid,string clientid)
        {
            var dal = new ProductsDAL();
            return dal.AddUnit(unitName, description, operateid, clientid);
        }
        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="attrName">属性名称</param>
        /// <param name="description">描述</param>
        /// <param name="operateid">操作人</param>
        /// <param name="clientid">客户端ID</param>
        /// <returns></returns>
        public string AddProductAttr(string attrName, string description, string operateid, string clientid)
        {
            var attrID = Guid.NewGuid().ToString();
            var dal = new ProductsDAL();
            if (dal.AddProductAttr(attrID, attrName, description, operateid, clientid))
            {
                return attrID.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 添加属性值
        /// </summary>
        /// <param name="valueName">值</param>
        /// <param name="attrID">属性ID</param>
        /// <returns></returns>
        public string AddAttrValue(string valueName, string attrID, string operateid, string clientid)
        {
            var valueID = Guid.NewGuid().ToString();
            var dal = new ProductsDAL();
            if (dal.AddAttrValue(valueID, valueName, attrID, operateid, clientid))
            {
                return valueID.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// 添加产品分类
        /// </summary>
        /// <param name="categoryCode">编码</param>
        /// <param name="categoryName">名称</param>
        /// <param name="pid">上级ID</param>
        /// <param name="status">状态</param>
        /// <param name="attrlist">规格参数</param>
        /// <param name="saleattr">销售属性</param>
        /// <param name="description">描述</param>
        /// <param name="operateid">操作人</param>
        /// <param name="clientid">客户端ID</param>
        /// <returns></returns>
        public string AddCategory(string categoryCode, string categoryName, string pid, int status, List<string> attrlist, List<string> saleattr, string description, string operateid, string clientid)
        {
            var dal = new ProductsDAL();
            return dal.AddCategory(categoryCode, categoryName, pid, status, string.Join(",", attrlist), string.Join(",", saleattr), description, operateid, clientid);
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

        /// <summary>
        /// 编辑品牌
        /// </summary>
        /// <param name="brandID">ID</param>
        /// <param name="name">名称</param>
        /// <param name="anotherName">别称</param>
        /// <param name="countryCode">国家编码</param>
        /// <param name="cityCode">城市编码</param>
        /// <param name="status">状态</param>
        /// <param name="remark">备注</param>
        /// <param name="brandStyle"风格></param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <returns></returns>
        public bool UpdateBrand(string brandID, string name, string anotherName, string countryCode, string cityCode, int status, string remark, string brandStyle, string operateIP, string operateID)
        {
            var dal = new ProductsDAL();
            return dal.UpdateBrand(brandID, name, anotherName, countryCode, cityCode, status, remark, brandStyle, operateIP, operateID);
        }

        /// <summary>
        /// 编辑单位
        /// </summary>
        /// <param name="unitID">单位ID</param>
        /// <param name="unitName">单位名称</param>
        /// <param name="desciption">描述</param>
        /// <param name="operateid">操作人</param>
        /// <returns></returns>
        public bool UpdateUnit(string unitID, string unitName, string desciption, string operateid)
        {
            var dal = new ProductsDAL();
            return dal.UpdateUnit(unitID, unitName, desciption);
        }

        /// <summary>
        /// 编辑单位状态
        /// </summary>
        /// <param name="unitID">单位ID</param>
        /// <param name="status">状态</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <returns></returns>
        public bool UpdateUnitStatus(string unitID, StatusEnum status, string operateIP, string operateID)
        {
            var dal = new ProductsDAL();
            return dal.UpdateUnitStatus(unitID, (int)status);
        }
        /// <summary>
        /// 编辑属性信息
        /// </summary>
        /// <param name="attrID">属性ID</param>
        /// <param name="attrName">属性名称</param>
        /// <param name="description">描述</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <returns></returns>
        public bool UpdateProductAttr(string attrID, string attrName, string description, string operateIP, string operateID)
        {
            var dal = new ProductsDAL();
            return dal.UpdateProductAttr(attrID, attrName, description);
        }

        /// <summary>
        /// 编辑属性值
        /// </summary>
        /// <param name="valueID">值ID</param>
        /// <param name="valueName">名称</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <returns></returns>
        public bool UpdateAttrValue(string valueID, string valueName, string operateIP, string operateID)
        {
            var dal = new ProductsDAL();
            return dal.UpdateAttrValue(valueID, valueName);
        }
        /// <summary>
        /// 编辑属性状态
        /// </summary>
        /// <param name="attrid">属性ID</param>
        /// <param name="status">状态</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <returns></returns>
        public bool UpdateProductAttrStatus(string attrid, StatusEnum status, string operateIP, string operateID)
        {
            var dal = new ProductsDAL();
            return dal.UpdateProductAttrStatus(attrid, (int)status);
        }
        /// <summary>
        /// 编辑属性值状态
        /// </summary>
        /// <param name="valueid">属性值ID</param>
        /// <param name="status">状态</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <returns></returns>
        public bool UpdateAttrValueStatus(string valueid, StatusEnum status, string operateIP, string operateID)
        {
            var dal = new ProductsDAL();
            return dal.UpdateAttrValueStatus(valueid, (int)status);
        }
        /// <summary>
        /// 编辑分类信息
        /// </summary>
        /// <param name="categoryid">分类ID</param>
        /// <param name="categoryName">名称</param>
        /// <param name="status">状态</param>
        /// <param name="attrlist">属性列表</param>
        /// <param name="description">描述</param>
        /// <param name="operateid">操作人</param>
        /// <returns></returns>
        public bool UpdateCategory(string categoryid, string categoryName,int status, List<string> attrlist, string description, string operateid)
        {
            var dal = new ProductsDAL();
            return dal.UpdateCategory(categoryid, categoryName, status, string.Join(",", attrlist), description);
        }

        #endregion
    }
}
