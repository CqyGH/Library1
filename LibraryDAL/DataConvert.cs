using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;

namespace LibraryDAL
{
    internal class DataConvert
    {
        /// <summary>
        /// 通过DataReader获取唯一结果
        /// </summary>
        /// <param name="reader">结果的DateReader</param>
        /// <returns>结果对象</returns>
        public T GetResult<T>(DbDataReader reader)
        {
            T result = default(T);
            if (reader.HasRows)
            {
                result = (T)Activator.CreateInstance(typeof(T));
                reader.Read();
                PropertyInfo[] infos = typeof(T).GetProperties();
                foreach (PropertyInfo info in infos)
                {
                    try
                    {
                        if (reader[info.Name] != null && reader[info.Name] != DBNull.Value)
                            result.GetType().GetProperty(info.Name).SetValue(result, Convert.ChangeType(reader[info.Name], info.PropertyType), null);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
                reader.Close();
                return result;
            }
            else
            {
                reader.Close();
                return result;
            }

        }

        /// <summary>
        ///  通过DataReader获取结果集
        /// </summary>
        /// <param name="reader">结果的DateReader</param>
        /// <returns>结果集对象</returns>
        public List<T> GetResultSet<T>(DbDataReader reader)
        {
            List<T> result = null;
            if (reader.HasRows)
            {
                result = new List<T>();
                while (reader.Read())
                {
                    T item = (T)Activator.CreateInstance(typeof(T));
                    PropertyInfo[] infos = typeof(T).GetProperties();
                    foreach (PropertyInfo info in infos)
                    {
                        try
                        {
                            if (reader[info.Name] != null && reader[info.Name] != DBNull.Value)
                                item.GetType().GetProperty(info.Name).SetValue(item, Convert.ChangeType(reader[info.Name], info.PropertyType), null);
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }
                    result.Add(item);
                }
            }
            reader.Close();
            return result;
        }

        /// <summary>
        /// 将对象转换为SQL参数列表
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <param name="prams">查询参数</param>
        /// <returns>SQL参数列表</returns>
        public DbParameter[] GetParameter(Object obj, Parameter[] prams)
        {
            List<DbParameter> param = new List<DbParameter>();
            foreach (var parameter in prams)
            {
                string dbType = ConfigurationManager.ConnectionStrings["DBString"].ProviderName;
                DbParameter p = null;
                switch (dbType)
                {
                    //case "MySql.Data.MySqlClient": p = new MySqlParameter(); break;
                    case "System.Data.SqlClient": p = new SqlParameter(); break;
                }
                if (p != null)
                {
                    p.ParameterName = "@" + parameter.ParameterName;
                    p.Value = obj.GetType().GetProperty(parameter.ParameterName).GetValue(obj, null);
                    param.Add(p);
                }
            }
            return param.ToArray();
        }

        /// <summary>
        /// 生成SQL参数列表
        /// </summary>
        /// <param name="conditions">参数值</param>
        /// <param name="prams">参数名称列表</param>
        /// <returns>>SQL参数列表</returns>
        public DbParameter[] GetParameter(Dictionary<string, string> conditions, Parameter[] prams)
        {
            List<DbParameter> param = new List<DbParameter>();
            foreach (var item in prams)
            {
                string dbType = ConfigurationManager.ConnectionStrings["DBString"].ProviderName;
                DbParameter p = null;
                switch (dbType)
                {
                    //case "MySql.Data.MySqlClient": p = new MySqlParameter(); break;
                    case "System.Data.SqlClient": p = new SqlParameter(); break;
                }
                string value;
                conditions.TryGetValue(item.ParameterName, out value);
                if (p != null)
                {
                    p.ParameterName = "@" + item.ParameterName;
                    switch (item.Type)
                    {
                        case "int": p.Value = Convert.ToInt32(value); break;
                        case "string": p.Value = value; break;
                        case "long": p.Value = Convert.ToInt64(value); break;
                        case "datetime": p.Value = Convert.ToDateTime(value); break;
                    }
                    param.Add(p);
                }
            }
            return param.ToArray();
        }

        public DbParameter[] GetParameter(Dictionary<string, string> conditions, String[] prams)
        {
            List<DbParameter> param = new List<DbParameter>();
            foreach (var item in prams)
            {
                string dbType = ConfigurationManager.ConnectionStrings["DBString"].ProviderName;
                DbParameter p = null;
                switch (dbType)
                {
                    //case "MySql.Data.MySqlClient": p = new MySqlParameter(); break;
                    case "System.Data.SqlClient": p = new SqlParameter(); break;
                }
                string value;
                conditions.TryGetValue(item, out value);
                if (p != null)
                {
                    p.ParameterName = "@" + item;
                    p.Value = value;
                    param.Add(p);
                }
            }
            return param.ToArray();
        }
    }
}
