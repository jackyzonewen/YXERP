/**  版本信息模板在安装目录下，可自行修改。
* Industry.cs
*
* 功 能： N/A
* 类 名： Industry
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
	/// Industry:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Industry
	{
		public Industry()
		{}
		#region Model
		private int _autoid;
		private string _industryid;
		private string _name;
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
        [Property("Lower")] 
		public string IndustryID
		{
			set{ _industryid=value;}
			get{return _industryid;}
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
        [Property("Lower")] 
		public string CreateUserID
		{
			set{ _createuserid=value;}
			get{return _createuserid;}
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

        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="dr"></param>
        public void FillData(System.Data.DataRow dr)
        {
            dr.FillData(this);
        }

	}
}

