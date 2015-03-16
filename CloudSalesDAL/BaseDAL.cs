using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CloudSalesDAL
{
    public abstract class BaseDAL
    {
        protected static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;

        /// <summary>
        /// 执行SQL语句返回影响的行数
        /// </summary>
        /// <param name="cmdText">SQL文本</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(string cmdText)
        {
            int retVal = 0;
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand comm = new SqlCommand();

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                comm.Connection = conn;
                comm.CommandText = cmdText;
                comm.CommandTimeout = 600;
                retVal = comm.ExecuteNonQuery();
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }

                return retVal;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语句返回影响的行数（带事物）
        /// </summary>
        /// <param name="tran">事物</param>
        /// <param name="cmdText">SQL文本</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(SqlTransaction tran, string cmdText)
        {
            int retVal = 0;
            SqlCommand comm = new SqlCommand();

            try
            {
                comm.Connection = tran.Connection;
                comm.CommandText = cmdText;
                comm.Transaction = tran;
                comm.CommandTimeout = 600;
                retVal = comm.ExecuteNonQuery();

                return retVal;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语句返回影响的行数（带参数）
        /// </summary>
        /// <param name="cmdText">SQL文本</param>
        /// <param name="parms">参数</param>
        /// <param name="cmdType">执行命令类型</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(string cmdText, SqlParameter[] parms, CommandType cmdType)
        {
            int retVal = 0;
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand comm = new SqlCommand();

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                comm.Connection = conn;
                comm.CommandText = cmdText;
                comm.CommandType = cmdType;
                comm.CommandTimeout = 600;

                foreach (SqlParameter parm in parms)
                {
                    comm.Parameters.Add(parm);
                }

                retVal = comm.ExecuteNonQuery();
                comm.Parameters.Clear();
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }

                return retVal;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语句返回影响的行数（带事物，参数）
        /// </summary>
        /// <param name="tran">事物</param>
        /// <param name="cmdText">SQL文本</param>
        /// <param name="parms">参数</param>
        /// <param name="cmdType">执行命令类型</param>
        /// <returns>影响行数</returns>
        public static int ExecuteNonQuery(SqlTransaction tran, string cmdText, SqlParameter[] parms, CommandType cmdType)
        {
            int retVal = 0;
            SqlCommand comm = new SqlCommand();

            try
            {
                comm.Connection = tran.Connection;
                comm.CommandText = cmdText;
                comm.CommandType = cmdType;
                comm.Transaction = tran;
                comm.CommandTimeout = 600;

                foreach (SqlParameter parm in parms)
                {
                    comm.Parameters.Add(parm);
                }

                retVal = comm.ExecuteNonQuery();
                comm.Parameters.Clear();

                return retVal;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语句返回结果的第一行第一列
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <returns>第一行第一列</returns>
        public static object ExecuteScalar(string cmdText)
        {
            object retVal;
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand comm = new SqlCommand();

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                comm.Connection = conn;
                comm.CommandText = cmdText;
                comm.CommandTimeout = 600;
                retVal = comm.ExecuteScalar();
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }

                return retVal;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语句返回结果的第一行第一列
        /// </summary>
        /// <param name="tran">事物</param>
        /// <param name="cmdText">SQL语句</param>
        /// <returns></returns>
        public static object ExecuteScalar(SqlTransaction tran, string cmdText)
        {
            object retVal;
            SqlCommand comm = new SqlCommand();

            try
            {
                comm.Connection = tran.Connection;
                comm.CommandText = cmdText;
                comm.Transaction = tran;
                comm.CommandTimeout = 600;
                retVal = comm.ExecuteScalar();

                return retVal;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语句返回结果的第一行第一列
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="parms">参数</param>
        /// <param name="cmdType">语句类型</param>
        /// <returns></returns>
        public static object ExecuteScalar(string cmdText, SqlParameter[] parms, CommandType cmdType)
        {
            object retVal;
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand comm = new SqlCommand();

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                comm.Connection = conn;
                comm.CommandText = cmdText;
                comm.CommandType = cmdType;
                comm.CommandTimeout = 600;

                foreach (SqlParameter parm in parms)
                {
                    comm.Parameters.Add(parm);
                }

                retVal = comm.ExecuteScalar();
                comm.Parameters.Clear();
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }

                return retVal;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语句返回结果的第一行第一列
        /// </summary>
        /// <param name="tran">事物</param>
        /// <param name="cmdText">命令文本</param>
        /// <param name="parms">参数</param>
        /// <param name="cmdType">文本类型</param>
        /// <returns></returns>
        public static object ExecuteScalar(SqlTransaction tran, string cmdText, SqlParameter[] parms, CommandType cmdType)
        {
            object retVal;
            SqlCommand comm = new SqlCommand();

            try
            {
                comm.Connection = tran.Connection;
                comm.CommandText = cmdText;
                comm.CommandType = cmdType;
                comm.Transaction = tran;
                comm.CommandTimeout = 600;

                foreach (SqlParameter parm in parms)
                {
                    comm.Parameters.Add(parm);
                }

                retVal = comm.ExecuteScalar();
                comm.Parameters.Clear();

                return retVal;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语句返回一个数据集
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <returns>数据集</returns>
        public static DataSet GetDataSet(string cmdText)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand comm = new SqlCommand();

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                comm.Connection = conn;
                comm.CommandText = cmdText;
                comm.CommandTimeout = 600;

                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(ds);
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }

                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语句返回一个数据集
        /// </summary>
        /// <param name="tran">事物</param>
        /// <param name="cmdText">SQL语句</param>
        /// <returns>数据集</returns>
        public static DataSet GetDataSet(SqlTransaction tran, string cmdText)
        {
            DataSet ds = new DataSet();
            SqlCommand comm = new SqlCommand();

            try
            {
                comm.Connection = tran.Connection;
                comm.CommandText = cmdText;
                comm.CommandTimeout = 600;
                comm.Transaction = tran;

                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(ds);

                return ds;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语句返回一个数据集
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="parms">参数</param>
        /// <param name="cmdType">命令类型</param>
        /// <returns>数据集</returns>
        public static DataSet GetDataSet(string cmdText, SqlParameter[] parms, CommandType cmdType)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand comm = new SqlCommand();

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                comm.Connection = conn;
                comm.CommandText = cmdText;
                comm.CommandTimeout = 600;
                comm.CommandType = cmdType;

                foreach (SqlParameter parm in parms)
                {
                    comm.Parameters.Add(parm);
                }

                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(ds);

                comm.Parameters.Clear();
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }

                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语句返回一个数据集
        /// </summary>
        /// <param name="tran">事物</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="parms">参数</param>
        /// <param name="cmdType">命令类型</param>
        /// <returns>数据集</returns>
        public static DataSet GetDataSet(SqlTransaction tran, string cmdText, SqlParameter[] parms, CommandType cmdType)
        {
            DataSet ds = new DataSet();
            SqlCommand comm = new SqlCommand();

            try
            {

                comm.Connection = tran.Connection;
                comm.CommandText = cmdText;
                comm.CommandTimeout = 600;
                comm.CommandType = cmdType;
                comm.Transaction = tran;

                foreach (SqlParameter parm in parms)
                {
                    comm.Parameters.Add(parm);
                }

                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(ds);

                comm.Parameters.Clear();

                return ds;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语句返回一个数据集
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <returns>数据集</returns>
        public static DataTable GetDataTable(string cmdText)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand comm = new SqlCommand();

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                comm.Connection = conn;
                comm.CommandText = cmdText;
                comm.CommandTimeout = 600;

                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(dt);
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }

                return dt;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语句返回一个数据集
        /// </summary>
        /// <param name="tran">事物</param>
        /// <param name="cmdText">SQL语句</param>
        /// <returns>数据集</returns>
        public static DataTable GetDataTable(SqlTransaction tran, string cmdText)
        {
            DataTable dt = new DataTable();
            SqlCommand comm = new SqlCommand();

            try
            {
                comm.Connection = tran.Connection;
                comm.CommandText = cmdText;
                comm.CommandTimeout = 600;
                comm.Transaction = tran;

                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(dt);

                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语句返回一个数据集
        /// </summary>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="parms">参数</param>
        /// <param name="cmdType">命令类型</param>
        /// <returns>数据集</returns>
        public static DataTable GetDataTable(string cmdText, SqlParameter[] parms, CommandType cmdType)
        {
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand comm = new SqlCommand();

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                comm.Connection = conn;
                comm.CommandText = cmdText;
                comm.CommandTimeout = 600;
                comm.CommandType = cmdType;

                foreach (SqlParameter parm in parms)
                {
                    comm.Parameters.Add(parm);
                }

                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(dt);

                comm.Parameters.Clear();
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }

                return dt;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语句返回一个数据集
        /// </summary>
        /// <param name="tran">事物</param>
        /// <param name="cmdText">SQL语句</param>
        /// <param name="parms">参数</param>
        /// <param name="cmdType">命令类型</param>
        /// <returns>数据集</returns>
        public static DataTable GetDataTable(SqlTransaction tran, string cmdText, SqlParameter[] parms, CommandType cmdType)
        {
            DataTable dt = new DataTable();
            SqlCommand comm = new SqlCommand();

            try
            {
                comm.Connection = tran.Connection;
                comm.CommandText = cmdText;
                comm.CommandTimeout = 600;
                comm.CommandType = cmdType;
                comm.Transaction = tran;

                foreach (SqlParameter parm in parms)
                {
                    comm.Parameters.Add(parm);
                }

                SqlDataAdapter sda = new SqlDataAdapter(comm);
                sda.Fill(dt);

                comm.Parameters.Clear();

                return dt;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语句返回DataReader
        /// </summary>
        /// <param name="cmdText">SQL文本</param>
        /// <returns>DataReader</returns>
        public static SqlDataReader ExecuteReader(string cmdText)
        {
            SqlDataReader Reader;
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand comm = new SqlCommand();

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                comm.Connection = conn;
                comm.CommandText = cmdText;
                comm.CommandTimeout = 600;
                Reader = comm.ExecuteReader(CommandBehavior.CloseConnection);

                return Reader;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语句返回DataReader（带事物）
        /// </summary>
        /// <param name="tran">事物</param>
        /// <param name="cmdText">SQL文本</param>
        /// <returns>DataReader</returns>
        public static SqlDataReader ExecuteReader(SqlTransaction tran, string cmdText)
        {
            SqlDataReader Reader;
            SqlCommand comm = new SqlCommand();

            try
            {
                comm.Connection = tran.Connection;
                comm.CommandText = cmdText;
                comm.Transaction = tran;
                comm.CommandTimeout = 600;
                Reader = comm.ExecuteReader();

                return Reader;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语句返回DataReader（带参数）
        /// </summary>
        /// <param name="cmdText">SQL文本</param>
        /// <param name="parms">参数</param>
        /// <param name="cmdType">执行命令类型</param>
        /// <returns>DataReader</returns>
        public static SqlDataReader ExecuteReader(string cmdText, SqlParameter[] parms, CommandType cmdType)
        {
            SqlDataReader Reader;
            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand comm = new SqlCommand();

            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                comm.Connection = conn;
                comm.CommandText = cmdText;
                comm.CommandType = cmdType;
                comm.CommandTimeout = 600;

                foreach (SqlParameter parm in parms)
                {
                    comm.Parameters.Add(parm);
                }

                Reader = comm.ExecuteReader(CommandBehavior.CloseConnection);
                comm.Parameters.Clear();

                return Reader;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行SQL语句返回DataReader（带事物，参数）
        /// </summary>
        /// <param name="tran">事物</param>
        /// <param name="cmdText">SQL文本</param>
        /// <param name="parms">参数</param>
        /// <param name="cmdType">执行命令类型</param>
        /// <returns>DataReader</returns>
        public static SqlDataReader ExecuteReader(SqlTransaction tran, string cmdText, SqlParameter[] parms, CommandType cmdType)
        {
            SqlDataReader Reader;
            SqlCommand comm = new SqlCommand();

            try
            {

                comm.Connection = tran.Connection;
                comm.CommandText = cmdText;
                comm.CommandType = cmdType;
                comm.Transaction = tran;
                comm.CommandTimeout = 600;

                foreach (SqlParameter parm in parms)
                {
                    comm.Parameters.Add(parm);
                }

                Reader = comm.ExecuteReader();
                comm.Parameters.Clear();

                return Reader;
            }
            catch
            {
                throw;
            }
        }
    }
}
