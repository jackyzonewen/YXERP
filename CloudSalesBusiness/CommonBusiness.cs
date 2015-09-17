using CloudSalesDAL;
using CloudSalesEntity;
using CloudSalesEnum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CloudSalesBusiness
{

    public class CommonBusiness
    {
        /// <summary>
        /// 获取地区列表
        /// </summary>
        /// <returns></returns>
        public static List<CityEntity> GetCitys()
        {
            return CommonCache.Citys;
        }

        /// <summary>
        /// 修改表中某字段值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">字段名</param>
        /// <param name="columnValue">字段值</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static bool Update(string tableName, string columnName, object columnValue, string where)
        {
            int obj = CommonDAL.Update(tableName, columnName, columnValue.ToString(), where);
            return obj > 0;
        }

        /// <summary>
        /// 获取表中某字段值
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columnName">字段名</param>
        /// <param name="columnValue">字段值</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static object Select(string tableName, string columnName, string where)
        {
            object obj = CommonDAL.Select(tableName, columnName, where);
            return obj;
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public static bool Delete(string tableName, string where)
        {
            int obj = CommonDAL.Delete(tableName, where);
            return obj > 0;
        }

        /// <summary>
        /// 获取分页数据集合
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">列明</param>
        /// <param name="condition">条件</param>
        /// <param name="key">主键，分页条件</param>
        /// <param name="orderColumn">排序字段</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCount">当前页数</param>
        /// <param name="totalNum">总记录数</param>
        /// <param name="totalPage">总页数</param>
        /// <param name="isAsc">主键是否升序</param>
        /// <returns></returns>
        public static DataTable GetPagerData(string tableName, string columns, string condition, string key, string orderColumn, int pageSize, int pageIndex, out int totalNum, out int pageCount, bool isAsc)
        {
            int asc = 0;
            if (isAsc)
            {
                asc = 1;
            }
            return CommonDAL.GetPagerData(tableName, columns, condition, key, orderColumn, pageSize, pageIndex, out totalNum, out pageCount, asc);
        }
        
        /// <summary>
        /// 获取分页数据集合(默认降序)
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">列明</param>
        /// <param name="condition">条件</param>
        /// <param name="key">主键，分页条件</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCount">当前页数</param>
        /// <param name="totalNum">总记录数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public static DataTable GetPagerData(string tableName, string columns, string condition, string key, int pageSize, int pageIndex, out int totalNum, out int pageCount)
        {
            return CommonDAL.GetPagerData(tableName, columns, condition, key, "", pageSize, pageIndex, out totalNum, out pageCount, 0);
        }
        
        /// <summary>
        /// 获取分页数据集合(默认降序)
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">列明</param>
        /// <param name="condition">条件</param>
        /// <param name="key">主键，分页条件</param>
        /// <param name="orderColumn">排序字段</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCount">当前页数</param>
        /// <param name="totalNum">总记录数</param>
        /// <param name="totalPage">总页数</param>
        /// <returns></returns>
        public static DataTable GetPagerData(string tableName, string columns, string condition, string key, string orderColumn, int pageSize, int pageIndex, out int totalNum, out int pageCount)
        {
            return CommonDAL.GetPagerData(tableName, columns, condition, key, orderColumn, pageSize, pageIndex, out totalNum, out pageCount, 0);
        }

        /// <summary>
        /// 获取分页数据集合
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="columns">列明</param>
        /// <param name="condition">条件</param>
        /// <param name="key">主键，分页条件</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageCount">当前页数</param>
        /// <param name="totalNum">总记录数</param>
        /// <param name="totalPage">总页数</param>
        /// <param name="isAsc">主键是否升序</param>
        /// <returns></returns>
        public static DataTable GetPagerData(string tableName, string columns, string condition, string key, int pageSize, int pageIndex, out int totalNum, out int pageCount, bool isAsc)
        {
            int asc = 0;
            if (isAsc)
            {
                asc = 1;
            }
            return CommonDAL.GetPagerData(tableName, columns, condition, key, "", pageSize, pageIndex, out totalNum, out pageCount, asc);
        }

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Enumtype"></param>
        /// <returns></returns>
        public static string GetEnumDesc<T>(T Enumtype)
        {
            if (Enumtype == null) throw new ArgumentNullException("Enumtype");
            if (!Enumtype.GetType().IsEnum) throw new Exception("参数类型不正确");
            return ((DescriptionAttribute)Enumtype.GetType().GetField(Enumtype.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description;
        }
    }
}
