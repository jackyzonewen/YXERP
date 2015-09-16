/**  版本信息模板在安装目录下，可自行修改。
* WareHouse.cs
*
* 功 能： N/A
* 类 名： WareHouse
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/5/3 13:38:26   N/A    初版
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
	/// WareHouse:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class WareHouse
	{
		public WareHouse()
		{}
		#region Model
		private int _autoid;
		private string _warecode;
		private string _name="";
		private string _shortname="";
		private string _typecode="";
		private string _countrycode="";
		private string _citycode="";
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
        public string WareID { get; set; }

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
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ShortName
		{
			set{ _shortname=value;}
			get{return _shortname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TypeCode
		{
			set{ _typecode=value;}
			get{return _typecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CountryCode
		{
			set{ _countrycode=value;}
			get{return _countrycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CityCode
		{
			set{ _citycode=value;}
			get{return _citycode;}
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

        public CityEntity City { get; set; }

        public void FillData(System.Data.DataRow dr)
        {
            dr.FillData(this);
        }

	}
}

