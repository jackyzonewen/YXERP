/**  版本信息模板在安装目录下，可自行修改。
* ProductDetail.cs
*
* 功 能： N/A
* 类 名： ProductDetail
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/5/3 13:38:18   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace CloudSalesEntity
{
	/// <summary>
	/// ProductDetail:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ProductDetail
	{
		public ProductDetail()
		{}
		#region Model
		private int _autoid;
		private string _productid;
		private string _saleattr="";
		private string _attrvalue="";
		private string _saleattrvalue="";
		private int? _salecount=0;
        private int? _stockin = 0;
        private int? _logicout = 0;
		private string _imgs="";
		private string _imgm="";
		private int? _warncount=0;
		private int? _status=1;
		private string _description="";
		private string _createuserid;
		private DateTime? _createtime= DateTime.Now;
		private DateTime? _updatetime= DateTime.Now;
		private string _operateip="";
		private string _clientid;
		/// <summary>
		/// 
		/// </summary>
		public int AutoID
		{
			set{ _autoid=value;}
			get{return _autoid;}
		}
        /// <summary>
        /// 
        /// </summary>
        [Property("Lower")] 
        public string ProductDetailID { set; get; }


        public string DetailsCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Property("Lower")] 
		public string ProductID
		{
			set{ _productid=value;}
			get{return _productid;}
		}
        /// <summary>
        /// 
        /// </summary>
        [Property("Lower")] 
		public string SaleAttr
		{
			set{ _saleattr=value;}
			get{return _saleattr;}
		}
        /// <summary>
        /// 
        /// </summary>
        [Property("Lower")] 
		public string AttrValue
		{
			set{ _attrvalue=value;}
			get{return _attrvalue;}
		}
        /// <summary>
        /// 
        /// </summary>
        [Property("Lower")] 
		public string SaleAttrValue
		{
			set{ _saleattrvalue=value;}
			get{return _saleattrvalue;}
		}

        public string SaleAttrValueString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Property("Lower")] 
        public string UnitID { get; set; }

        public ProductUnit Unit { get; set; }

		/// <summary>
		/// 
		/// </summary>
        public int? SaleCount
		{
			set{ _salecount=value;}
			get{return _salecount;}
		}
		/// <summary>
		/// 
		/// </summary>
        public int? StockIn
		{
			set{ _stockin=value;}
			get{return _stockin;}
		}
		/// <summary>
		/// 
		/// </summary>
        public int? LogicOut
		{
			set{ _logicout=value;}
			get{return _logicout;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImgS
		{
			set{ _imgs=value;}
			get{return _imgs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImgM
		{
			set{ _imgm=value;}
			get{return _imgm;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? WarnCount
		{
			set{ _warncount=value;}
			get{return _warncount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
        /// <summary>
        /// 
        /// </summary>
        [Property("Lower")] 
		public string CreateUserID
		{
			set{ _createuserid=value;}
			get{return _createuserid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UpdateTime
		{
			set{ _updatetime=value;}
			get{return _updatetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperateIP
		{
			set{ _operateip=value;}
			get{return _operateip;}
		}
        /// <summary>
        /// 
        /// </summary>
        [Property("Lower")] 
		public string ClientID
		{
			set{ _clientid=value;}
			get{return _clientid;}
		}
		#endregion Model


        public string ShapeCode { get; set; }

        public decimal Price { get; set; }

        public decimal BigPrice { get; set; }

        public decimal Weight { get; set; }

        public string ProductName { get; set; }

        public string UnitName { get; set; }

        public int Quantity { get; set; }

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

