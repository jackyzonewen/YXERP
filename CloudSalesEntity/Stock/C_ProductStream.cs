/**  版本信息模板在安装目录下，可自行修改。
* C_ProductStream.cs
*
* 功 能： N/A
* 类 名： C_ProductStream
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
	/// C_ProductStream:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class C_ProductStream
	{
		public C_ProductStream()
		{}
		#region Model
		private int _autoid;
		private string _productid;
		private string _ordercode;
		private string _batchcode="";
		private string _orderdate;
		private int? _ordertype=0;
		private int? _quantity=0;
		private int? _surplusquantity=0;
		private string _warecode="";
		private string _depotcode="";
		private string _createuserid;
		private DateTime? _createtime= DateTime.Now;
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
        public string ProductDetailID { set; get; }
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
		public string OrderCode
		{
			set{ _ordercode=value;}
			get{return _ordercode;}
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
		public string OrderDate
		{
			set{ _orderdate=value;}
			get{return _orderdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? OrderType
		{
			set{ _ordertype=value;}
			get{return _ordertype;}
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
		public int? SurplusQuantity
		{
			set{ _surplusquantity=value;}
			get{return _surplusquantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WareCode
		{
			set{ _warecode=value;}
			get{return _warecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DepotCode
		{
			set{ _depotcode=value;}
			get{return _depotcode;}
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

