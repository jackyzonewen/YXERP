using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using CloudSalesDAL;
using CloudSalesEntity;


namespace CloudSalesBusiness
{
    /// <summary>
    /// 公共数据缓存
    /// </summary>
    public class CommonCache
    {
        private static Dictionary<string, List<Menu>> _modules;
        /// <summary>
        /// 模块
        /// </summary>
        public static Dictionary<string, List<Menu>> Modules
        {
            get 
            {
                if (_modules == null)
                {
                    DataSet ds = new CommonDAL().GetModulesMenus();
                    _modules = new Dictionary<string, List<Menu>>();
                    if (ds.Tables.Contains("Modules") && ds.Tables.Contains("Menus"))
                    {
                        foreach (DataRow dr in ds.Tables["Modules"].Rows)
                        {
                            M_Modules model = new M_Modules();
                            model.FillData(dr);
                            List<Menu> list = new List<Menu>();
                            foreach (DataRow menu in ds.Tables["Menus"].Select("ModulesID='" + model.ModulesID + "'"))
                            {
                                Menu mModel = new Menu();
                                mModel.FillData(menu);
                                list.Add(mModel);
                            }
                            _modules.Add(model.ModulesID, list);
                        }
                    }
                }
                return _modules;
            }
        }

        private static Dictionary<string, List<Menu>> _clientMenus;
        /// <summary>
        /// 客户端菜单
        /// </summary>
        public static Dictionary<string, List<Menu>> ClientMenus
        {
            get
            {
                if (_clientMenus == null)
                {
                    _clientMenus = new Dictionary<string, List<Menu>>();
                }
                return _clientMenus;
            }
            set
            {
                _clientMenus = value;
            }
        }

        private static List<CityEntity> _citys;
        /// <summary>
        /// 城市
        /// </summary>
        public static List<CityEntity> Citys
        {
            get
            {
                if (_citys == null)
                {
                    DataTable dt = new CommonDAL().GetCitys();
                    _citys = new List<CityEntity>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        CityEntity model = new CityEntity();
                        model.FillData(dr);
                        _citys.Add(model);
                    }
                }
                return _citys;
            }
        }

        private static Dictionary<string, List<C_Industry>> _industry;

        /// <summary>
        /// 行业
        /// </summary>
        public static Dictionary<string, List<C_Industry>> Industry
        {
            get
            {
                if (_industry == null)
                {
                    _industry = new Dictionary<string, List<C_Industry>>();
                }
                return _industry;
            }
            set
            {
                _industry = value;
            }
        }
    }
}
