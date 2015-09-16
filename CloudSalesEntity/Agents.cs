/**  版本信息模板在安装目录下，可自行修改。
* C_Agents.cs
*
* 功 能： N/A
* 类 名： C_Agents
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/5/3 13:38:15   N/A    初版
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
	/// C_Agents:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Agents
	{
		public Agents()
		{}
		#region Model
		private int _autoid;
		private string _agentid;
		private string _companyname="";
		private string _industryid;
		private string _citycode="";
		private string _address="";
		private string _postalcode="";
		private string _contactname="";
		private string _mobilephone="";
		private string _officephone="";
		private int? _status=0;
		private DateTime? _createtime= DateTime.Now;
		private string _createuserid;
		private string _parentid;
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
		public string AgentID
		{
			set{ _agentid=value;}
			get{return _agentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CompanyName
		{
			set{ _companyname=value;}
			get{return _companyname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IndustryID
		{
			set{ _industryid=value;}
			get{return _industryid;}
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
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PostalCode
		{
			set{ _postalcode=value;}
			get{return _postalcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ContactName
		{
			set{ _contactname=value;}
			get{return _contactname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MobilePhone
		{
			set{ _mobilephone=value;}
			get{return _mobilephone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OfficePhone
		{
			set{ _officephone=value;}
			get{return _officephone;}
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
		public DateTime? CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
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
		public string ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
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

