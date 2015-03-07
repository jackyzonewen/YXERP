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
        private static List<Menu> _menus;
        /// <summary>
        /// 菜单
        /// </summary>
        public static List<Menu> Menus
        {
            get 
            {
                if (_menus == null)
                {
                    DataTable dt = new CommonDAL().GetMenus();
                    _menus = new List<Menu>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        Menu model = new Menu();
                        model.FillData(dr);
                        _menus.Add(model);
                    }
                }
                return _menus;
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
    }
}
