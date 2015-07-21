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

        #region 库别-弃用

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

        #region 查询

        /// <summary>
        /// 获取仓库列表
        /// </summary>
        /// <param name="keyWords">关键词</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="totalCount">总记录数</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="clientID">客户端ID</param>
        /// <returns></returns>
        public List<C_WareHouse> GetWareHouses(string keyWords, int pageSize, int pageIndex, ref int totalCount, ref int pageCount, string clientID)
        {
            var dal = new WarehouseDAL();
            DataSet ds = dal.GetWareHouses(keyWords, pageSize, pageIndex, ref totalCount, ref pageCount, clientID);

            List<C_WareHouse> list = new List<C_WareHouse>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                C_WareHouse model = new C_WareHouse();
                model.FillData(dr);
                model.City = CommonCache.Citys.Where(c => c.CityCode == model.CityCode).FirstOrDefault();
                list.Add(model);
            }
            return list;
        }

        #endregion

        #region 添加

        /// <summary>
        /// 添加仓库
        /// </summary>
        /// <param name="warecode">仓库编码</param>
        /// <param name="name">名称</param>
        /// <param name="shortname">简称</param>
        /// <param name="citycode">所在地区编码</param>
        /// <param name="status">状态</param>
        /// <param name="description">描述</param>
        /// <param name="operateid">操作人</param>
        /// <param name="clientid">客户端ID</param>
        /// <returns></returns>
        public string AddWareHouse(string warecode, string name, string shortname, string citycode, int status, string description, string operateid, string clientid)
        {
            var id = Guid.NewGuid().ToString();
            var dal = new WarehouseDAL();
            if (dal.AddWareHouse(id, warecode, name, shortname, citycode, status, description, operateid, clientid))
            {
                return id.ToString();
            }
            return string.Empty;
        }
        #endregion

        #region 编辑、删除
        /// <summary>
        /// 编辑仓库
        /// </summary>
        /// <param name="id">仓库ID</param>
        /// <param name="name">名称</param>
        /// <param name="shortname">简称</param>
        /// <param name="citycode">地区编码</param>
        /// <param name="status">状态</param>
        /// <param name="description">描述</param>
        /// <param name="operateid">操作人</param>
        /// <param name="clientid">客户端ID</param>
        /// <returns></returns>
        public bool UpdateWareHouse(string id, string name, string shortname, string citycode, int status, string description, string operateid, string clientid)
        {

            var dal = new WarehouseDAL();
            return dal.UpdateWareHouse(id, name, shortname, citycode, status, description);
        }

        /// <summary>
        /// 编辑仓库状态
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="operateid"></param>
        /// <param name="clientid"></param>
        /// <returns></returns>
        public bool UpdateWareHouseStatus(string id, StatusEnum status, string operateid, string clientid)
        {
            return CommonBusiness.Update("C_WareHouse", "Status", (int)status, " WareID='" + id + "'");
        }

        #endregion
    }
}
