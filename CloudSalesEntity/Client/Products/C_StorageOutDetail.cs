/**  版本信息模板在安装目录下，可自行修改。
* C_StorageOutDetail.cs
*
* 功 能： N/A
* 类 名： C_StorageOutDetail
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/5/3 13:38:22   N/A    初版
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
	/// C_StorageOutDetail:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class C_StorageOutDetail
	{
		public C_StorageOutDetail()
		{}
		#region Model
		private int _autoid;
		private string _docid;
		private string _productcode;
		private string _productid;
		private string _docdate;
		private string _produceddate;
		private int? _quantity=0;
		private decimal? _commonprice=0M;
		private decimal? _price=0M;
		private decimal? _totalmoney=0M;
		private decimal? _profit=0M;
		private decimal? _taxmoney=0M;
		private decimal? _taxrate=1M;
		private decimal? _returnprice=0M;
		private decimal? _returnmoney=0M;
		private string _batchcode="";
		private DateTime? _effectivedate;
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
		public string DocID
		{
			set{ _docid=value;}
			get{return _docid;}
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
		public string ProductID
		{
			set{ _productid=value;}
			get{return _productid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DocDate
		{
			set{ _docdate=value;}
			get{return _docdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProducedDate
		{
			set{ _produceddate=value;}
			get{return _produceddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Quantity
		{
			set{ _quantity=value;}
			get{return _quantity;}
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
		public decimal? Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? TotalMoney
		{
			set{ _totalmoney=value;}
			get{return _totalmoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Profit
		{
			set{ _profit=value;}
			get{return _profit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? TaxMoney
		{
			set{ _taxmoney=value;}
			get{return _taxmoney;}
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
		public decimal? ReturnPrice
		{
			set{ _returnprice=value;}
			get{return _returnprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ReturnMoney
		{
			set{ _returnmoney=value;}
			get{return _returnmoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BatchCode
		{
			set{ _batchcode=value;}
			get{return _batchcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EffectiveDate
		{
			set{ _effectivedate=value;}
			get{return _effectivedate;}
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

