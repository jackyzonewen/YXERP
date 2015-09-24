using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CloudSalesDAL
{
    public class ClientDAL : BaseDAL
    {
        public static ClientDAL BaseProvider = new ClientDAL();
        #region 查询
        public DataTable GetClientDetail(string clientID)
        {

            SqlParameter[] paras = { 
                                    new SqlParameter("@ClientID",clientID),
                                   };
            return GetDataTable("select * from Clients where ClientID=@ClientID and Status<>9", paras, CommandType.Text);
        }
        #endregion

        #region 添加

        public string InsertClient(string companyName, string contactName, string mobilePhone, string industry, string cityCode, string address,
                                   string description, string loginName, string loginPwd, string modules, string userid, out int result)
        {
            string clientid = Guid.NewGuid().ToString();
            result = 0;
            SqlParameter[] parms = { 
                                       new SqlParameter("@Result",result),
                                       new SqlParameter("@ClientiD",clientid),
                                       new SqlParameter("@CompanyName",companyName),
                                       new SqlParameter("@MobilePhone",mobilePhone),
                                       new SqlParameter("@Industry",industry),
                                       new SqlParameter("@CityCode",cityCode),
                                       new SqlParameter("@Address",address),
                                       new SqlParameter("@Description",description),
                                       new SqlParameter("@ContactName",contactName),
                                       new SqlParameter("@LoginName",loginName),
                                       new SqlParameter("@LoginPWD",loginPwd),
                                       new SqlParameter("@Modules",modules),
                                       new SqlParameter("@CreateUserID",userid)
                                   };
            parms[0].Direction = ParameterDirection.Output;

            ExecuteNonQuery("P_InsertClient", parms, CommandType.StoredProcedure);

            result = Convert.ToInt32(parms[0].Value);
            return clientid;
        }


        #endregion

        #region 编辑
        public bool UpdateClient(string clientID, string companyName, string contactName, string mobilePhone, string industry, string cityCode, string address,string description, string loginName, string loginPwd, string modules, string userid)
        {
            SqlParameter[] parms = { 
                                       new SqlParameter("@ClientiD",clientID),
                                       new SqlParameter("@CompanyName",companyName),
                                       new SqlParameter("@MobilePhone",mobilePhone),
                                       new SqlParameter("@Industry",industry),
                                       new SqlParameter("@CityCode",cityCode),
                                       new SqlParameter("@Address",address),
                                       new SqlParameter("@Description",description),
                                       new SqlParameter("@ContactName",contactName)
                                   };

            string cmdText = "update Clients set CompanyName=@CompanyName,MobilePhone=@MobilePhone,Industry=@Industry, CityCode=@CityCode, Address=@Address,Description=@Description,ContactName=@ContactName where ClientiD=@ClientiD";

            return ExecuteNonQuery(cmdText, parms, CommandType.Text) > 0;
        }

        public bool ClientAuthorize(string clientID,int userQuantity, int authorizeType, DateTime endTime)
        { 
        SqlParameter[] parms = { 
                                       new SqlParameter("@ClientiD",clientID),
                                       new SqlParameter("@UserQuantity",userQuantity),
                                       new SqlParameter("@AuthorizeType",authorizeType),
                                       new SqlParameter("@EndTime",endTime)
                                   };

        string cmdText = "update Clients set  UserQuantity=@UserQuantity,AuthorizeType=@AuthorizeType,EndTime=@EndTime where              ClientiD=@ClientiD";

        return ExecuteNonQuery(cmdText, parms, CommandType.Text) > 0;
        }
            #endregion
    }
}
