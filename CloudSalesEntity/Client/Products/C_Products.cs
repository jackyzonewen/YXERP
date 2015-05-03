/**  版本信息模板在安装目录下，可自行修改。
* C_Products.cs
*
* 功 能： N/A
* 类 名： C_Products
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/5/3 13:38:19   N/A    初版
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
	/// C_Products:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class C_Products
	{
		public C_Products()
		{}
		#region Model
		private int _autoid;
		private string _productcode;
		private string _productname="";
		private string _generalname="";
		private string _mnemoniccode="";
		private int? _iscombineproduct=0;
		private string _brandid;
		private string _bigunitid;
		private string _smallunitid;
		private int? _bigsmallmultiple=1;
		private string _categorycode="";
		private string _categorycodelist=",";
		private string _saleattr=",";
		private string _attrlist=",";
		private string _valuelist=",";
		private string _attrvaluelist=",";
		private decimal? _commonprice=0M;
		private decimal? _preferentialprice=0M;
		private decimal? _preferentialpv=0M;
		private decimal? _taxrate=1M;
		private int? _status=1;
		private DateTime? _onlinetime= DateTime.Now;
		private int? _usetype=0;
		private int? _isnew=0;
		private int? _isrecommend=0;
		private int? _isdiscount=0;
		private decimal? _discountvalue=1M;
		private int? _salecount=0;
		private decimal? _weight=0M;
		private string _productimage="";
		private int? _effectivedays=0;
		private string _shapecode="";
		private string _prodiverid;
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
		public string ProductCode
		{
			set{ _productcode=value;}
			get{return _productcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProductName
		{
			set{ _productname=value;}
			get{return _productname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GeneralName
		{
			set{ _generalname=value;}
			get{return _generalname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MnemonicCode
		{
			set{ _mnemoniccode=value;}
			get{return _mnemoniccode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsCombineProduct
		{
			set{ _iscombineproduct=value;}
			get{return _iscombineproduct;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BrandID
		{
			set{ _brandid=value;}
			get{return _brandid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BigUnitID
		{
			set{ _bigunitid=value;}
			get{return _bigunitid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SmallUnitID
		{
			set{ _smallunitid=value;}
			get{return _smallunitid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BigSmallMultiple
		{
			set{ _bigsmallmultiple=value;}
			get{return _bigsmallmultiple;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CategoryCode
		{
			set{ _categorycode=value;}
			get{return _categorycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CategoryCodeList
		{
			set{ _categorycodelist=value;}
			get{return _categorycodelist;}
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
		public string AttrList
		{
			set{ _attrlist=value;}
			get{return _attrlist;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ValueList
		{
			set{ _valuelist=value;}
			get{return _valuelist;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AttrValueList
		{
			set{ _attrvaluelist=value;}
			get{return _attrvaluelist;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? CommonPrice
		{
			set{ _commonprice=value;}
			get{return _commonprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? PreferentialPrice
		{
			set{ _preferentialprice=value;}
			get{return _preferentialprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? PreferentialPV
		{
			set{ _preferentialpv=value;}
			get{return _preferentialpv;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? TaxRate
		{
			set{ _taxrate=value;}
			get{return _taxrate;}
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
		public DateTime? OnlineTime
		{
			set{ _onlinetime=value;}
			get{return _onlinetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? UseType
		{
			set{ _usetype=value;}
			get{return _usetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsNew
		{
			set{ _isnew=value;}
			get{return _isnew;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsRecommend
		{
			set{ _isrecommend=value;}
			get{return _isrecommend;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsDiscount
		{
			set{ _isdiscount=value;}
			get{return _isdiscount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? DiscountValue
		{
			set{ _discountvalue=value;}
			get{return _discountvalue;}
		}
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
		public decimal? Weight
		{
			set{ _weight=value;}
			get{return _weight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProductImage
		{
			set{ _productimage=value;}
			get{return _productimage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EffectiveDays
		{
			set{ _effectivedays=value;}
			get{return _effectivedays;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShapeCode
		{
			set{ _shapecode=value;}
			get{return _shapecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProdiverID
		{
			set{ _prodiverid=value;}
			get{return _prodiverid;}
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

