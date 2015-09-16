/**  版本信息模板在安装目录下，可自行修改。
* Department.cs
*
* 功 能： N/A
* 类 名： Department
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/4/8 19:58:54   N/A    初版
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
	/// Department:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Department
	{
		public Department()
		{}
		#region Model
		private int _autoid;
		private string _departid;
		private string _name;
		private string _parentid;
		private int? _status=0;
		private string _description="";
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
		public string DepartID
		{
			set{ _departid=value;}
			get{return _departid;}
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
		public string ParentID
		{
			set{ _parentid=value;}
			get{return _parentid;}
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

        public void FillData(System.Data.DataRow dr)
        {
            dr.FillData(this);
        }

	}
}

