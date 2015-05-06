using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        /// 产品单位列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Unit() 
        {
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
        public ActionResult ProductsAdd() 
        {
            return View();
        }

        /// <summary>
        /// 产品列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductsList() 
        {
            return View();
        }
    }
}
