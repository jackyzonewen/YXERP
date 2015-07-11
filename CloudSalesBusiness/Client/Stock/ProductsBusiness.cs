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
        /// <summary>
        /// 文件默认存储路径
        /// </summary>
        public const string FILEPATH = "/Content/uploadFiles/";

        public static object SingleLock = new object();

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

        public List<C_Brand> GetBrandList(string clientID)
        {
            var dal = new ProductsDAL();
            DataTable dt = dal.GetBrandList(clientID);

            List<C_Brand> list = new List<C_Brand>();
            foreach (DataRow dr in dt.Rows)
            {
                C_Brand model = new C_Brand();
                model.FillData(dr);
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

        /// <summary>
        /// 获取产品分类详情（包括属性和值）
        /// </summary>
        /// <param name="categoryid">分类ID</param>
        /// <returns></returns>
        public C_Category GetCategoryDetailByID(string categoryid)
        {
            var dal = new ProductsDAL();
            DataSet ds = dal.GetCategoryDetailByID(categoryid);

            C_Category model = new C_Category();
            if (ds.Tables.Contains("Category") && ds.Tables["Category"].Rows.Count > 0)
            {
                model.FillData(ds.Tables["Category"].Rows[0]);
                List<C_ProductAttr> salelist = new List<C_ProductAttr>();
                List<C_ProductAttr> attrlist = new List<C_ProductAttr>();
                bool bl = false;
                foreach (DataRow attr in ds.Tables["Attrs"].Rows)
                {
                    bl = false;
                    C_ProductAttr modelattr = new C_ProductAttr();
                    modelattr.FillData(attr);
                    if (model.AttrList.ToLower().IndexOf(modelattr.AttrID.ToLower()) >= 0)
                    {
                        bl = true;
                        attrlist.Add(modelattr);
                    }
                    if (model.SaleAttr.ToLower().IndexOf(modelattr.AttrID.ToLower()) >= 0)
                    {
                        bl = true;
                        salelist.Add(modelattr);
                    }
                    if (bl)
                    {
                        modelattr.AttrValues = new List<C_AttrValue>();
                        foreach (DataRow value in ds.Tables["Values"].Select("AttrID='" + modelattr.AttrID + "'"))
                        {
                            C_AttrValue valuemodel = new C_AttrValue();
                            valuemodel.FillData(value);
                            modelattr.AttrValues.Add(valuemodel);
                        }
                    }
                }

                model.SaleAttrs = salelist;
                model.AttrLists = attrlist;
            }

            return model;
        }

        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <param name="keyWords">关键词</param>
        /// <param name="pageSize">页Size</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="totalCount">总数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="clientID">客户端ID</param>
        /// <returns></returns>
        public List<C_Products> GetProductList(string keyWords, int pageSize, int pageIndex, ref int totalCount, ref int pageCount, string clientID)
        {
            var dal = new ProductsDAL();
            DataSet ds = dal.GetProductList(keyWords, pageSize, pageIndex, ref totalCount, ref pageCount, clientID);

            List<C_Products> list = new List<C_Products>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                C_Products model = new C_Products();
                model.FillData(dr);
                list.Add(model);
            }
            return list;
        }

        /// <summary>
        /// 根据产品ID获取产品信息
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public C_Products GetProductByID(string productid)
        {
            var dal = new ProductsDAL();
            DataSet ds = dal.GetProductByID(productid);

            C_Products model = new C_Products();
            if (ds.Tables.Contains("Product") && ds.Tables["Product"].Rows.Count > 0)
            {
                model.FillData(ds.Tables["Product"].Rows[0]);
                List<C_ProductDetail> list = new List<C_ProductDetail>();
                foreach (DataRow item in ds.Tables["Details"].Rows)
                {
                    C_ProductDetail detail = new C_ProductDetail();
                    detail.FillData(item);
                    list.Add(detail);
                }
                model.ProductDetails = list;
            }

            return model;
        }

        /// <summary>
        /// 是否存在产品编码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool IsExistProductCode(string code, string clientid)
        {
            object obj = CommonBusiness.Select("C_Products", " Count(0) ", "ClientID='" + clientid + "' and ProductCode='" + code + "'");
            return Convert.ToInt32(obj) > 0;
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
            lock (SingleLock)
            {
                if (!string.IsNullOrEmpty(icoPath))
                {
                    if (icoPath.IndexOf("?") > 0)
                    {
                        icoPath = icoPath.Substring(0, icoPath.IndexOf("?"));
                    }
                    FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(icoPath));
                    icoPath = FILEPATH + file.Name;
                    if (file.Exists)
                    {
                        file.MoveTo(HttpContext.Current.Server.MapPath(icoPath));
                    }
                }
                else
                {
                    icoPath = FILEPATH + DateTime.Now.ToString("yyyyMMddHHmmssms") + new Random().Next(1000, 9999).ToString() + ".png";
                }
                return new ProductsDAL().AddBrand(name, anotherName, icoPath, countryCode, cityCode, status, remark, brandStyle, operateIP, operateID, clientID);
            }
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

        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="productCode">产品编码</param>
        /// <param name="productName">产品名称</param>
        /// <param name="generalName">常用名</param>
        /// <param name="iscombineproduct">是否组合产品</param>
        /// <param name="brandid">品牌ID</param>
        /// <param name="bigunitid">大单位</param>
        /// <param name="smallunitid">小单位</param>
        /// <param name="bigSmallMultiple">大小单位比例</param>
        /// <param name="categoryid">分类ID</param>
        /// <param name="status">状态</param>
        /// <param name="attrlist">属性列表</param>
        /// <param name="valuelist">值列表</param>
        /// <param name="attrvaluelist">属性值键值对</param>
        /// <param name="commonprice">原价</param>
        /// <param name="price">优惠价</param>
        /// <param name="weight">重量</param>
        /// <param name="isnew">是否新品</param>
        /// <param name="isRecommend">是否推荐</param>
        /// <param name="effectiveDays">有效期天数</param>
        /// <param name="discountValue">折扣</param>
        /// <param name="productImg">产品图片</param>
        /// <param name="description">描述</param>
        /// <param name="operateid">操作人</param>
        /// <param name="clientid">客户端ID</param>
        /// <returns></returns>
        public string AddProduct(string productCode, string productName, string generalName, bool iscombineproduct, string brandid, string bigunitid, string smallunitid, int bigSmallMultiple,
                                 string categoryid, int status, string attrlist, string valuelist, string attrvaluelist, decimal commonprice, decimal price, decimal weight, bool isnew,
                                 bool isRecommend, int effectiveDays, decimal discountValue, string productImg, string shapeCode, string description, string operateid, string clientid)
        {
            lock (SingleLock)
            {
                if (!string.IsNullOrEmpty(productImg))
                {
                    if (productImg.IndexOf("?") > 0)
                    {
                        productImg = productImg.Substring(0, productImg.IndexOf("?"));
                    }
                    FileInfo file = new FileInfo(HttpContext.Current.Server.MapPath(productImg));
                    productImg = FILEPATH + file.Name;
                    if (file.Exists)
                    {
                        file.MoveTo(HttpContext.Current.Server.MapPath(productImg));
                    }
                }
                else
                {
                    productImg = FILEPATH + DateTime.Now.ToString("yyyyMMddHHmmssms") + new Random().Next(1000, 9999).ToString() + ".png";
                }

                var dal = new ProductsDAL();
                return dal.AddProduct(productCode, productName, generalName, iscombineproduct, brandid, bigunitid, smallunitid, bigSmallMultiple, categoryid, status, attrlist,
                                        valuelist, attrvaluelist, commonprice, price, weight, isnew, isRecommend, effectiveDays, discountValue, productImg, shapeCode, description, operateid, clientid);
            }
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
        public bool UpdateCategory(string categoryid, string categoryName, int status, List<string> attrlist, List<string> saleattr, string description, string operateid)
        {
            var dal = new ProductsDAL();
            return dal.UpdateCategory(categoryid, categoryName, status, string.Join(",", attrlist), string.Join(",", saleattr), description);
        }

        /// <summary>
        /// 编辑产品状态
        /// </summary>
        /// <param name="productid">产品ID</param>
        /// <param name="status">状态</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <returns></returns>
        public bool UpdateProductStatus(string productid, StatusEnum status, string operateIP, string operateID)
        {
            return CommonBusiness.Update("C_Products", "Status", ((int)status).ToString(), " ProductID='" + productid + "'");
        }
        /// <summary>
        /// 编辑产品是否新品
        /// </summary>
        /// <param name="productid">产品ID</param>
        /// <param name="isNew">true 新品</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <returns></returns>
        public bool UpdateProductIsNew(string productid, bool isNew, string operateIP, string operateID)
        {
            return CommonBusiness.Update("C_Products", "IsNew", isNew ? "1" : "0", " ProductID='" + productid + "'");
        }
        /// <summary>
        /// 编辑产品是否推荐
        /// </summary>
        /// <param name="productid">产品ID</param>
        /// <param name="isRecommend">true 推荐</param>
        /// <param name="operateIP">操作IP</param>
        /// <param name="operateID">操作人</param>
        /// <returns></returns>
        public bool UpdateProductIsRecommend(string productid, bool isRecommend, string operateIP, string operateID)
        {
            return CommonBusiness.Update("C_Products", "IsRecommend", isRecommend ? "1" : "0", " ProductID='" + productid + "'");
        }

        /// <summary>
        /// 编辑产品信息
        /// </summary>
        /// <param name="productid">产品ID</param>
        /// <param name="productCode">产品编码</param>
        /// <param name="productName">产品名称</param>
        /// <param name="generalName">常用名</param>
        /// <param name="iscombineproduct">是否组合产品</param>
        /// <param name="brandid">品牌ID</param>
        /// <param name="bigunitid">大单位</param>
        /// <param name="smallunitid">小单位</param>
        /// <param name="bigSmallMultiple">大小单位比例</param>
        /// <param name="status">状态</param>
        /// <param name="attrlist">属性列表</param>
        /// <param name="valuelist">值列表</param>
        /// <param name="attrvaluelist">属性值键值对</param>
        /// <param name="commonprice">原价</param>
        /// <param name="price">优惠价</param>
        /// <param name="weight">重量</param>
        /// <param name="isnew">是否新品</param>
        /// <param name="isRecommend">是否推荐</param>
        /// <param name="effectiveDays">有效期天数</param>
        /// <param name="discountValue">折扣</param>
        /// <param name="description">描述</param>
        /// <param name="operateid">操作人</param>
        /// <param name="clientid">客户端ID</param>
        /// <returns></returns>
        public bool UpdateProduct(string productid,string productCode, string productName, string generalName, bool iscombineproduct, string brandid, string bigunitid, string smallunitid, int bigSmallMultiple,
                         int status, string categoryid, string attrlist, string valuelist, string attrvaluelist, decimal commonprice, decimal price, decimal weight, bool isnew,
                         bool isRecommend, int effectiveDays, decimal discountValue, string shapeCode, string description, string operateid, string clientid)
        {

            var dal = new ProductsDAL();
            return dal.UpdateProduct(productid, productCode, productName, generalName, iscombineproduct, brandid, bigunitid, smallunitid, bigSmallMultiple, status, categoryid,attrlist,
                                    valuelist, attrvaluelist, commonprice, price, weight, isnew, isRecommend, effectiveDays, discountValue, shapeCode, description, operateid, clientid);
        }

        #endregion
    }
}
