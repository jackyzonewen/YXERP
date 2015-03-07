/**  版本信息模板在安装目录下，可自行修改。
* Country.cs
*
* 功 能： N/A
* 类 名： Country
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/3/6 19:52:51   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
namespace CloudSalesEntity
{
	/// <summary>
	/// Country:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Country
	{
		public Country()
		{}
		#region Model
		private int _autoid;
		private string _countrycode;
		private string _name;
		private string _englishname="";
		private string _phonecode="";
		private string _language="";
		private int? _currency=0;
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
		public string CountryCode
		{
			set{ _countrycode=value;}
			get{return _countrycode;}
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
		public string EnglishName
		{
			set{ _englishname=value;}
			get{return _englishname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PhoneCode
		{
			set{ _phonecode=value;}
			get{return _phonecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Language
		{
			set{ _language=value;}
			get{return _language;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Currency
		{
			set{ _currency=value;}
			get{return _currency;}
		}
		#endregion Model

        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="dr"></param>
        public void FillData(DataRow dr)
        {
            dr.FillData(this);
        }
	}
}

