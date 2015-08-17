/**  版本信息模板在安装目录下，可自行修改。
* C_ProductUnit.cs
*
* 功 能： N/A
* 类 名： C_ProductUnit
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/5/3 13:38:20   N/A    初版
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
	/// C_ProductUnit:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class C_ProductUnit
	{
		public C_ProductUnit()
		{}
		#region Model
		private int _autoid;
		private string _unitid;
		private string _unitname="";
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
		public string UnitID
		{
			set{ _unitid=value;}
			get{return _unitid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UnitName
		{
			set{ _unitname=value;}
			get{return _unitname;}
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

        public void FillData(System.Data.DataRow dr)
        {
            dr.FillData(this);
        }

	}
}

