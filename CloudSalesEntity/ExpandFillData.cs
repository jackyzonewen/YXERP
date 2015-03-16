using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CloudSalesEntity
{
    public static class ExpandFillData
    {
        /// <summary>
        /// 实体填充
        /// </summary>
        public static void FillData<T>(this DataRow dr, T entity)
        {
            Type type = typeof(T);
            //表字段集合
            DataColumnCollection cl = dr.Table.Columns;
            //对象属性列表
            PropertyInfo[] propertys = type.GetProperties();
            foreach (PropertyInfo property in propertys)
            {
                if (cl.Contains(property.Name))
                {
                    switch (property.PropertyType.Name)
                    {
                        case "Int32":
                            property.SetValue(entity,
                                dr[property.Name] != null && dr[property.Name] != DBNull.Value
                                ? Convert.ToInt32(dr[property.Name])
                                : -1,
                                null);
                            break;
                        case "String":
                            property.SetValue(entity,
                                dr[property.Name] != null && dr[property.Name] != DBNull.Value 
                                ? dr[property.Name].ToString() 
                                : "",
                                null);
                            break;
                        default:
                            property.SetValue(entity, dr[property.Name], null);
                            break;
                    }
                }

            }
        }
    }
}
