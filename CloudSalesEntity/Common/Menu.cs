/**  版本信息模板在安装目录下，可自行修改。
* Menu.cs
*
* 功 能： N/A
* 类 名： Menu
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
using System.Collections.Generic;
using System.Data;
namespace CloudSalesEntity
{
	/// <summary>
	/// Menu:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Menu
	{
		public Menu()
		{}
		#region Model
		private int _autoid;
		private string _menucode;
		private string _name;
		private string _area="";
		private string _controller="";
		private string _view="";
		private string _icopath="";
		private int? _type=1;
		private int? _ishide=0;
		private string _pcode="";
		private int? _sort=0;
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
		public string MenuCode
		{
			set{ _menucode=value;}
			get{return _menucode;}
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
		public string Area
		{
			set{ _area=value;}
			get{return _area;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Controller
		{
			set{ _controller=value;}
			get{return _controller;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string View
		{
			set{ _view=value;}
			get{return _view;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IcoPath
		{
			set{ _icopath=value;}
			get{return _icopath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? IsHide
		{
			set{ _ishide=value;}
			get{return _ishide;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PCode
		{
			set{ _pcode=value;}
			get{return _pcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
        /// <summary>
        /// 菜单包含权限
        /// </summary>
        public List<Permission> Permission { get; set; }

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

