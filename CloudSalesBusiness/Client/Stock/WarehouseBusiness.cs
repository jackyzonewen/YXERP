using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;


using CloudSalesDAL;
using CloudSalesEntity;
using CloudSalesEnum;

namespace CloudSalesBusiness
{
    public class WarehouseBusiness
    {

        public static object SingleLock = new object();

        #region 查询

        /// <summary>
        /// 获取库别列表
        /// </summary>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public List<C_WareHouseType> GetWarehouseTypes(string clientid)
        { 
            var dal = new WarehouseDAL();
            DataTable dt = dal.GetWarehouseTypes(clientid);
            List<C_WareHouseType> list = new List<C_WareHouseType>();
            foreach (DataRow item in dt.Rows)
            {
                C_WareHouseType model = new C_WareHouseType();
                model.FillData(item);
                list.Add(model);
            }
            return list;
        }

        #endregion

        #region 添加

        /// <summary>
        /// 添加库别
        /// </summary>
        /// <param name="name">库别名称</param>
        /// <param name="description">描述</param>
        /// <param name="operateid">操作人</param>
        /// <param name="clientid">客户端ID</param>
        /// <returns></returns>
        public string AddWarehouseType(string name, string description, string operateid, string clientid)
        {
            var id = Guid.NewGuid().ToString();
            var dal = new WarehouseDAL();
            if (dal.AddWarehouseType(id, name, description, operateid, clientid))
            {
                return id.ToString();
            }
            return string.Empty;
        }

        #endregion

        #region 编辑、删除

        /// <summary>
        /// 编辑库别
        /// </summary>
        /// <param name="id">库别ID</param>
        /// <param name="name">库别名称</param>
        /// <param name="description">描述</param>
        /// <param name="operateid">操作人</param>
        /// <param name="clientid">客户端ID</param>
        /// <returns></returns>
        public bool UpdateWarehouseType(string id, string name, string description, string operateid, string clientid)
        {
            var dal = new WarehouseDAL();
            return dal.UpdateWarehouseType(id, name, description);
           
        }
        /// <summary>
        /// 删除库别
        /// </summary>
        /// <param name="id">库别ID</param>
        /// <param name="operateid">操作人</param>
        /// <param name="clientid">客户端ID</param>
        /// <returns></returns>
        public bool DeleteWarehouseType(string id, string operateid, string clientid)
        {
            return CommonBusiness.Update("C_WareHouseType", "Status", (int)CloudSalesEnum.StatusEnum.Delete, " TypeID='" + id + "'");
        }

        #endregion
    }
}
