/**  版本信息模板在安装目录下，可自行修改。
* C_ProductDetail.cs
*
* 功 能： N/A
* 类 名： C_ProductDetail
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
	/// C_ProductDetail:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class C_ProductDetail
	{
		public C_ProductDetail()
		{}
		#region Model
		private int _autoid;
		private string _productid;
		private string _detailid;
		private string _saleattr=",";
		private string _attrvalue=",";
		private string _saleattrvalue=",";
		private long? _salecount=0;
		private long? _stockin=0;
		private long? _logicout=0;
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
        public string DetailID
        {
            set { _detailid = value; }
            get { return _detailid; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string ProductID
		{
			set{ _productid=value;}
			get{return _productid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SaleAttr
		{
			set{ _saleattr=value;}
			get{return _saleattr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AttrValue
		{
			set{ _attrvalue=value;}
			get{return _attrvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SaleAttrValue
		{
			set{ _saleattrvalue=value;}
			get{return _saleattrvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? SaleCount
		{
			set{ _salecount=value;}
			get{return _salecount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? StockIn
		{
			set{ _stockin=value;}
			get{return _stockin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public long? LogicOut
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
		public string ClientID
		{
			set{ _clientid=value;}
			get{return _clientid;}
		}
		#endregion Model

	}
}

