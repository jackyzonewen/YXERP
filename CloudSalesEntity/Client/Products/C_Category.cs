/**  版本信息模板在安装目录下，可自行修改。
* C_Category.cs
*
* 功 能： N/A
* 类 名： C_Category
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/5/3 13:38:16   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
namespace CloudSalesEntity
{
	/// <summary>
	/// C_Category:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    [Serializable]
    public partial class C_Category
    {
        public C_Category()
        { }
        #region Model
        private int _autoid;
        private string _categorycode;
        private string _categoryname = "";
        private string _pcode = "";
        private string _pcodelist = "";
        private int? _layers = 1;
        private string _saleattr = "";
        private string _attrlist = "";
        private string _brandlist = "";
        private int? _status = 1;
        private string _description = "";
        private string _createuserid;
        private DateTime? _createtime = DateTime.Now;
        private DateTime? _updatetime = DateTime.Now;
        private string _operateip = "";
        private string _clientid;
        /// <summary>
        /// 
        /// </summary>
        public int AutoID
        {
            set { _autoid = value; }
            get { return _autoid; }
        }
        public string CategoryID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CategoryCode
        {
            set { _categorycode = value; }
            get { return _categorycode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CategoryName
        {
            set { _categoryname = value; }
            get { return _categoryname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PID
        {
            set { _pcode = value; }
            get { return _pcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PIDList
        {
            set { _pcodelist = value; }
            get { return _pcodelist; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Layers
        {
            set { _layers = value; }
            get { return _layers; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SaleAttr
        {
            set { _saleattr = value; }
            get { return _saleattr; }
        }

        public List<C_ProductAttr> SaleAttrs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AttrList
        {
            set { _attrlist = value; }
            get { return _attrlist; }
        }

        public List<C_ProductAttr> AttrLists { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BrandList
        {
            set { _brandlist = value; }
            get { return _brandlist; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreateUserID
        {
            set { _createuserid = value; }
            get { return _createuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OperateIP
        {
            set { _operateip = value; }
            get { return _operateip; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ClientID
        {
            set { _clientid = value; }
            get { return _clientid; }
        }
        #endregion Model


        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="dr"></param>
        public void FillData(System.Data.DataRow dr)
        {
            dr.FillData(this);
        }

    }
}

