using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CloudSalesDAL
{
    public class ModulesDAL : BaseDAL
    {
        #region 查询

        public DataTable GetModules()
        {
            return GetDataTable("select * from Modules Order by Sort");
        }

        #endregion
    }
}
