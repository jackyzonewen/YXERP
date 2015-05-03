/**  版本信息模板在安装目录下，可自行修改。
* M_ModulesMenu.cs
*
* 功 能： N/A
* 类 名： M_ModulesMenu
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/3/6 19:52:53   N/A    初版
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
	/// M_ModulesMenu:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class M_ModulesMenu
	{
		public M_ModulesMenu()
		{}
		#region Model
		private int _autoid;
		private string _modulesid;
		private string _menucode;
		private DateTime? _createtime= DateTime.Now;
		private string _createuserid;
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
		public string ModulesID
		{
			set{ _modulesid=value;}
			get{return _modulesid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MenuCode
		{
			set{ _menucode=value;}
			get{return _menucode;}
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
		#endregion Model

	}
}

