/**  版本信息模板在安装目录下，可自行修改。
* C_UserDepart.cs
*
* 功 能： N/A
* 类 名： C_UserDepart
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/4/8 19:58:55   N/A    初版
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
	/// C_UserDepart:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class C_UserDepart
	{
		public C_UserDepart()
		{}
		#region Model
		private int _autoid;
		private string _userid;
		private string _departid;
		private DateTime? _createtime= DateTime.Now;
		private string _createuserid;
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
		public string UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DepartID
		{
			set{ _departid=value;}
			get{return _departid;}
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
		public string ClientID
		{
			set{ _clientid=value;}
			get{return _clientid;}
		}
		#endregion Model

	}
}

