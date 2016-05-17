using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace LibraryDAL.DBHelper
{
    internal class SqlServerHelper : IDBHelper
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["DBString"].ConnectionString;
        /// <summary>
        /// 获取数据库的连接对象
        /// </summary>
        /// <returns></returns>
        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
        /// <summary>
        /// 获取数据库命令对象
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="type">命令类型</param>
        /// <param name="sql">SQL语句</param>
        /// <returns>数据库命令对象</returns>
        private SqlCommand GetCommand(SqlConnection conn, CommandType type, string sql)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = type;
            return cmd;
        }
        /// <summary>
        /// 获取带参数的数据库命令对象
        /// </summary>
        /// <param name="conn">数据库连接</param>
        /// <param name="type">命令类型</param>
        /// <param name="sql">SQL语句</param>
        /// <param name="prams">参数</param>
        /// <returns>数据库命令对象</returns>
        private SqlCommand GetCommand(SqlConnection conn, CommandType type, string sql, DbParameter[] prams)
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = type;
            cmd.Parameters.AddRange(prams);
            return cmd;
        }

        public int ExecuteCommand(string sql, DbParameter[] prams, CommandType type)
        {
            int result;
            SqlTransaction trans = null;
            SqlConnection conn = GetConnection();
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                SqlCommand cmd = GetCommand(conn, type, sql, prams);
                cmd.Transaction = trans;
                result = cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception)
            {
                if (trans != null) trans.Rollback();
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public int ExecuteCommand(string sql, CommandType type)
        {
            int result;
            SqlTransaction trans = null;
            SqlConnection conn = GetConnection();
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                SqlCommand cmd = GetCommand(conn, type, sql);
                cmd.Transaction = trans;
                result = cmd.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception)
            {
                if (trans != null) trans.Rollback();
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public DbDataReader GetDataReader(string sql, CommandType type, DbParameter[] prams)
        {
            SqlDataReader reader;
            SqlConnection conn = null;
            try
            {
                conn = GetConnection();
                conn.Open();
                SqlCommand cmd = GetCommand(conn, type, sql, prams);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
                if (conn != null) conn.Close();
                throw;
            }
            return reader;
        }

        public DbDataReader GetDataReader(string sql, CommandType type)
        {
            SqlDataReader reader;
            SqlConnection conn = null;
            try
            {
                conn = GetConnection();
                conn.Open();
                SqlCommand cmd = GetCommand(conn, type, sql);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
                if (conn != null) conn.Close();
                throw;
            }
            return reader;
        }

        public object GetScalar(string sql, CommandType type, DbParameter[] prams)
        {
            object result;
            SqlConnection conn = null;
            try
            {
                conn = GetConnection();
                conn.Open();
                SqlCommand cmd = GetCommand(conn, type, sql, prams);
                result = cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                if (conn != null) conn.Close();
                throw;
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return result;
        }

        public object GetScalar(string sql, CommandType type)
        {
            object result;
            SqlConnection conn = null;
            try
            {
                conn = GetConnection();
                conn.Open();
                SqlCommand cmd = GetCommand(conn, type, sql);
                result = cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                if (conn != null) conn.Close();
                throw;
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return result;
        }
    }
}
