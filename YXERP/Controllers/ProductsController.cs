using CloudSalesBusiness;
using CloudSalesEntity;
using CloudSalesEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace YXERP.Controllers
{
    public class ProductsController : BaseController
    {
        //
        // GET: /Products/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 品牌列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Brand() 
        {
            return View();
        }

        /// <summary>
        /// 添加品牌
        /// </summary>
        /// <returns></returns>
        public ActionResult BrandAdd() 
        {
            return View();
        }
        /// <summary>
        /// 品牌详情
        /// </summary>
        /// <returns></returns>
        public ActionResult BrandDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View("Brand");
            }
            C_Brand model = new ProductsBusiness().GetBrandByBrandID(id);
            ViewBag.Item = model;
            ViewBag.ID = id;
           
            return View();
        }

        /// <summary>
        /// 产品单位列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Unit() 
        {
            ViewBag.Items = new ProductsBusiness().GetClientUnits(CurrentUser.ClientID);
            return View();
        }

        /// <summary>
        /// 产品分类列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Category() 
        {
            return View();
        }

        /// <summary>
        /// 产品属性
        /// </summary>
        /// <returns></returns>
        public ActionResult Attr() 
        {
            return View();
        }

        /// <summary>
        /// 添加产品
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductAdd() 
        {
            return View();
        }

        /// <summary>
        /// 产品列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductList() 
        {
            return View();
        }


        #region Ajax

        #region 品牌
        /// <summary>
        /// 保存品牌
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public JsonResult SavaBrand(string brand)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            C_Brand model = serializer.Deserialize<C_Brand>(brand);

            string brandID = "";
            if (string.IsNullOrEmpty(model.BrandID))
            {
                brandID = new ProductsBusiness().AddBrand(model.Name, model.AnotherName, model.IcoPath, model.CountryCode, model.CityCode, model.Status.Value, model.Remark, model.BrandStyle, OperateIP, CurrentUser.UserID, CurrentUser.ClientID);
            }
            else
            {
                bool bl = new ProductsBusiness().UpdateBrand(model.BrandID, model.Name, model.AnotherName, model.CountryCode, model.CityCode, model.Status.Value, model.Remark, model.BrandStyle, OperateIP, CurrentUser.UserID);
                if (bl)
                {
                    brandID = model.BrandID;
                }
            }
            JsonDictionary.Add("ID", brandID);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 获取品牌列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetBrandList(string keyWords, int pageSize, int pageIndex, int totalCount)
        {
            int pageCount = 0;
            List<C_Brand> list = new ProductsBusiness().GetBrandList(keyWords, pageSize, pageIndex, ref totalCount, ref pageCount, CurrentUser.ClientID);
            JsonDictionary.Add("Items", list);
            JsonDictionary.Add("TotalCount", totalCount);
            JsonDictionary.Add("PageCount", pageCount);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 编辑品牌状态
        /// </summary>
        /// <returns></returns>
        public JsonResult UpdateBrandStatus(string brandID, int status)
        {
            bool bl = new ProductsBusiness().UpdateBrandStatus(brandID, (StatusEnum)status, OperateIP, CurrentUser.UserID);
            JsonDictionary.Add("Status", bl);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        /// <summary>
        /// 删除品牌
        /// </summary>
        /// <returns></returns>
        public JsonResult DeleteBrand(string brandID)
        {
            bool bl = new ProductsBusiness().UpdateBrandStatus(brandID, StatusEnum.Delete, OperateIP, CurrentUser.UserID);
            JsonDictionary.Add("Status", bl);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 获取品牌详细信息
        /// </summary>
        /// <returns></returns>
        public JsonResult GetBrandDetail(string brandID)
        {
            C_Brand model = new ProductsBusiness().GetBrandByBrandID(brandID);
            JsonDictionary.Add("Item", model);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion

        #region 单位

        /// <summary>
        /// 保存单位
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public JsonResult SaveUnit(string unit)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            C_ProductUnit model = serializer.Deserialize<C_ProductUnit>(unit);

            string UnitID = "";
            if (string.IsNullOrEmpty(model.UnitID))
            {
                UnitID = new ProductsBusiness().AddUnit(model.UnitName, model.Description, CurrentUser.UserID, CurrentUser.ClientID);
            }
            else
            {
                bool bl = new ProductsBusiness().UpdateUnit(model.UnitID, model.UnitName, model.Description, CurrentUser.UserID);
                if (bl)
                {
                    UnitID = model.UnitID;
                }
            }
            JsonDictionary.Add("ID", UnitID);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        /// <summary>
        /// 删除单位
        /// </summary>
        /// <returns></returns>
        public JsonResult DeleteUnit(string unitID)
        {
            bool bl = new ProductsBusiness().UpdateUnitStatus(unitID, StatusEnum.Delete, OperateIP, CurrentUser.UserID);
            JsonDictionary.Add("Status", bl);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        #endregion

        #region 属性

        /// <summary>
        /// 获取属性列表
        /// </summary>
        /// <param name="index"></param>
        /// <param name="keyWorks"></param>
        /// <returns></returns>
        public JsonResult GetAttrList(int index, string keyWorks)
        {
            List<C_ProductAttr> list = new List<C_ProductAttr>();

            int totalCount = 0, pageCount = 0;
            list = new ProductsBusiness().GetAttrList(keyWorks, PageSize, index, ref totalCount, ref pageCount, CurrentUser.ClientID);

            JsonDictionary.Add("Items", list);
            JsonDictionary.Add("TotalCount", totalCount);
            JsonDictionary.Add("PageCount", pageCount);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        /// <summary>
        /// 获取所有属性
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllAttrList()
        {
            List<C_ProductAttr> list = new List<C_ProductAttr>();
            list = new ProductsBusiness().GetAttrList(CurrentUser.ClientID);

            JsonDictionary.Add("Items", list);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 保存属性
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public JsonResult SaveAttr(string attr)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            C_ProductAttr model = serializer.Deserialize<C_ProductAttr>(attr);

            string attrID = string.Empty;
            if (string.IsNullOrEmpty(model.AttrID))
            {
                attrID = new ProductsBusiness().AddProductAttr(model.AttrName, model.Description, CurrentUser.UserID, CurrentUser.ClientID);
            }
            else if (new ProductsBusiness().UpdateProductAttr(model.AttrID, model.AttrName, model.Description, OperateIP, CurrentUser.UserID))
            {
                attrID = model.AttrID.ToString();
            }


            JsonDictionary.Add("ID", attrID);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 获取属性详情
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public JsonResult GetAttrByID(string attrID = "")
        {
            if (string.IsNullOrEmpty(attrID))
            {
                JsonDictionary.Add("Item", null);
            }
            else
            {
                var model = new ProductsBusiness().GetProductAttrByID(attrID);
                JsonDictionary.Add("Item", model);
            }
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 保存属性值
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public JsonResult SaveAttrValue(string value)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            C_AttrValue model = serializer.Deserialize<C_AttrValue>(value);

            string valueID = string.Empty;
            if (!string.IsNullOrEmpty(model.AttrID))
            {
                if (string.IsNullOrEmpty(model.ValueID))
                {
                    valueID = new ProductsBusiness().AddAttrValue(model.ValueName, model.AttrID, CurrentUser.UserID, CurrentUser.ClientID);
                }
                else if (new ProductsBusiness().UpdateAttrValue(model.ValueID, model.ValueName, OperateIP, CurrentUser.UserID))
                {
                    valueID = model.ValueID.ToString();
                }
            }

            JsonDictionary.Add("ID", valueID);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 删除属性
        /// </summary>
        /// <param name="attrid"></param>
        /// <returns></returns>
        public JsonResult DeleteProductAttr(string attrid)
        {
            bool bl = new ProductsBusiness().UpdateProductAttrStatus(attrid, StatusEnum.Delete, OperateIP, CurrentUser.UserID);
            JsonDictionary.Add("Status", bl);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        /// <summary>
        /// 删除属性值
        /// </summary>
        /// <param name="valueid"></param>
        /// <returns></returns>
        public JsonResult DeleteAttrValue(string valueid)
        {
            bool bl = new ProductsBusiness().UpdateAttrValueStatus(valueid, StatusEnum.Delete, OperateIP, CurrentUser.UserID);
            JsonDictionary.Add("Status", bl);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion

        #region 分类

        /// <summary>
        /// 保存分类
        /// </summary>
        /// <param name="category"></param>
        /// <param name="saleattr"></param>
        /// <param name="attrlist"></param>
        /// <returns></returns>
        public JsonResult SavaCategory(string category, List<string> saleattr, List<string> attrlist)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            C_Category model = serializer.Deserialize<C_Category>(category);

            string caregoryid = "";
            if (string.IsNullOrEmpty(model.CategoryID))
            {
                caregoryid = new ProductsBusiness().AddCategory(model.CategoryCode, model.CategoryName, model.PID, model.Status.Value, attrlist, saleattr, model.Description, CurrentUser.UserID, CurrentUser.ClientID);
            }
            else
            {
                bool bl = true;
                if (bl)
                {
                    caregoryid = model.CategoryID;
                }
            }
            JsonDictionary.Add("ID", caregoryid);
            return new JsonResult
            {
                Data = JsonDictionary,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion

        #endregion
    }
}
