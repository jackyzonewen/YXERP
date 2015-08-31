using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using CloudSalesEntity;
using CloudSalesDAL;


namespace CloudSalesBusiness
{
    public class OrganizationBusiness
    {
        #region 查询

        /// <summary>
        /// 客户端账号是否存在
        /// </summary>
        /// <param name="loginName">账号</param>
        /// <returns></returns>
        public static bool IsExistLoginName(string loginName)
        {
            object count = CommonBusiness.Select("C_Users", "count(0)", "LoginName='" + loginName + "'");
            return Convert.ToInt32(count) > 0;
        }

        /// <summary>
        /// 根据用户名密码获取会员信息（登录）
        /// </summary>
        /// <param name="loginname">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns></returns>
        public static C_Users GetC_UserByUserName(string loginname, string pwd, string operateip)
        {
            pwd = CloudSalesTool.Encrypt.GetEncryptPwd(pwd, loginname);
            DataSet ds = new OrganizationDAL().GetC_UserByUserName(loginname, pwd);
            C_Users model = null;
            if (ds.Tables.Contains("User") && ds.Tables["User"].Rows.Count > 0)
            {
                model = new C_Users();
                model.FillData(ds.Tables["User"].Rows[0]);

                if (CommonCache.ClientMenus.ContainsKey(model.ClientID))
                {
                    model.Menus = CommonCache.ClientMenus[model.ClientID];
                }
                else if (ds.Tables.Contains("Modules"))
                {
                    List<Menu> list = new List<Menu>();
                    var modules = CommonCache.Modules;
                    foreach (DataRow dr in ds.Tables["Modules"].Rows)
                    {
                        M_Modules module = new M_Modules();
                        module.FillData(dr);
                        if (modules.ContainsKey(module.ModulesID))
                        {
                            foreach (var item in modules[module.ModulesID])
                            {
                                if (list.Where(m => m.MenuCode == item.MenuCode).Count() == 0)
                                {
                                    list.Add(item);
                                }
                            }
                        }
                    }
                    list = list.OrderBy(m => m.Sort).ToList();
                    CommonCache.ClientMenus.Add(model.ClientID, list);
                    model.Menus = list;
                }
            }

            //记录登录日志
            LogBusiness.AddLoginLog(loginname, model != null, CloudSalesEnum.EnumSystemType.Client, operateip);

            return model;
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="clientid">客户端ID</param>
        /// <returns></returns>
        public static List<C_Department> GetDepartments(string clientid)
        {
            DataTable dt = new OrganizationDAL().GetDepartments(clientid);
            List<C_Department> list = new List<C_Department>();
            foreach (DataRow dr in dt.Rows)
            {
                C_Department model = new C_Department();
                model.FillData(dr);
                list.Add(model);
            }
            return list;
        }

        #endregion

        #region 添加

        /// <summary>
        /// 添加部门
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="parentid">上级ID</param>
        /// <param name="description">描述</param>
        /// <param name="operateid">操作人</param>
        /// <param name="clientid">客户端ID</param>
        /// <returns></returns>
        public string AddDepartment(string name, string parentid, string description, string operateid, string clientid)
        {
            var dal = new OrganizationDAL();
            return dal.AddDepartment(name, parentid, description, operateid, clientid);
        }

        #endregion

        #region 编辑

        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <param name="departid">部门ID</param>
        /// <param name="name">名称</param>
        /// <param name="description">描述</param>
        /// <param name="operateid">操作人</param>
        /// <param name="operateip">操作IP</param>
        /// <returns></returns>
        public bool UpdateDepartment(string departid, string name, string description, string operateid, string operateip)
        {
            var dal = new OrganizationDAL();
            return dal.UpdateDepartment(departid, name, description);
        }

        #endregion

        #region 删除
        #endregion
    }
}
