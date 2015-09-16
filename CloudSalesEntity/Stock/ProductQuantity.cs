/**  版本信息模板在安装目录下，可自行修改。
* ProductQuantity.cs
*
* 功 能： N/A
* 类 名： ProductQuantity
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
	/// ProductQuantity:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ProductQuantity
	{
		public ProductQuantity()
		{}
		#region Model
		private int _autoid;
		private string _productid;
		private string _produceddate;
		private long? _stockin=0;
		private long? _stockout=0;
		private string _batchcode="";
		private string _warecode="";
		private string _depotcode="";
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
		public string ProducedDate
		{
			set{ _produceddate=value;}
			get{return _produceddate;}
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
		public long? StockOut
		{
			set{ _stockout=value;}
			get{return _stockout;}
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
		public string ClientID
		{
			set{ _clientid=value;}
			get{return _clientid;}
		}
		#endregion Model

	}
}

