using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSalesDAL
{
    public class OrganizationDAL :BaseDAL
    {
        #region 查询

        public DataSet GetC_UserByUserName(string loginname, string pwd)
        {

            SqlParameter[] paras = { 
                                    new SqlParameter("@LoginName",loginname),
                                    new SqlParameter("@LoginPwd",pwd)
                                   };
            return GetDataSet("P_GetC_UserToLogin", paras, CommandType.StoredProcedure, "User|Department|Role|Permission|Modules");
        }

        public DataTable GetDepartments(string clientid)
        {
            string sql = "select * from C_Department where ClientID=@ClientID and Status<>9";

            SqlParameter[] paras = { 
                                    new SqlParameter("@ClientID",clientid)
                                   };

            return GetDataTable(sql, paras, CommandType.Text);
        }

        #endregion

        #region 添加

        public string AddDepartment(string name, string parentid, string description, string operateid, string clientid)
        {
            string id = Guid.NewGuid().ToString();
            string sql = "insert into C_Department(DepartID,Name,ParentID,Status,Description,CreateUserID,ClientID) "+
                        " values(@DepartID,@Name,@ParentID,1,@Description,@CreateUserID,@ClientID)";

            SqlParameter[] paras = { 
                                       new SqlParameter("@DepartID",id),
                                       new SqlParameter("@Name",name),
                                       new SqlParameter("@ParentID",parentid),
                                       new SqlParameter("@Description",description),
                                       new SqlParameter("@CreateUserID",operateid),
                                       new SqlParameter("@ClientID",clientid)
                                   };

            if (ExecuteNonQuery(sql, paras, CommandType.Text) > 0)
            {
                return id;
            }
            return "";
        }

        #endregion

        #region 编辑

        public bool UpdateDepartment(string departid, string name, string description)
        {
            string sql = "update C_Department set Name=@Name,Description=@Description where DepartID=@DepartID";

            SqlParameter[] paras = { 
                                       new SqlParameter("@DepartID",departid),
                                       new SqlParameter("@Name",name),
                                       new SqlParameter("@Description",description)
                                   };

            return ExecuteNonQuery(sql, paras, CommandType.Text) > 0;
        }


        #endregion

        #region 删除
        #endregion
    }
}
