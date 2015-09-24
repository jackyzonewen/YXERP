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

        public DataTable GetModulesByClientID(string clientID)
        {
            SqlParameter[] paras = { 
                                    new SqlParameter("@ClientID",clientID),
                                   };
            return GetDataTable("select c.*,m.Name,m.Description from ClientModules as c,Modules as m where c.ModulesID=m.ModulesID and c.ClientID=@ClientID", paras, CommandType.Text);
        } 
        #endregion
    }
}
