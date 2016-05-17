using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using LibraryDAL.DBHelper;

namespace LibraryDAL
{
    public class DBContext
    {


        public List<T> GetDataList<T>(string queryName, Dictionary<string, string> conditions, string whereString, List<Parameter> paramsList)
        {
            var type = typeof(T);
            List<Query> collection = new QueryCollection().GetQuerySet(type.Name);
            Query query = (from q in collection
                           where q.Name == queryName
                           select q).FirstOrDefault();
            if (query != null)
            {
                DbParameter[] paramList = new DataConvert().GetParameter(conditions, paramsList.ToArray());
                string sql = query.SQL;
                sql = string.Format(sql,  whereString);
                try
                {
                    IDBHelper helper = DBHelperFactory.GetHelper();
                    DbDataReader reader = helper.GetDataReader(sql, CommandType.Text, paramList);
                    return new DataConvert().GetResultSet<T>(reader);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="queryName">查询名称</param>
        /// <param name="obj">数据对象</param>
        /// <returns>新增结果</returns>
        public bool InsertData<T>(string queryName, Object obj)
        {
            var type = typeof(T);
            List<Query> collection = new QueryCollection().GetQuerySet(type.Name);
            Query query = (from q in collection
                           where q.Name == queryName
                           select q).FirstOrDefault();
            DbParameter[] paramList = new DataConvert().GetParameter(obj, query.Parameters.ToArray());
            try
            {
                IDBHelper helper = DBHelperFactory.GetHelper();
                int affectRow = helper.ExecuteCommand(query.SQL, paramList, CommandType.Text);
                if (affectRow > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="queryName">查询名称</param>
        /// <param name="obj">数据对象</param>
        /// <returns>更新结果</returns>
        public bool UpdateData<T>(string queryName, Object obj)
        {
            var type = typeof(T);
            List<Query> collection = new QueryCollection().GetQuerySet(type.Name);
            Query query = (from q in collection
                           where q.Name == queryName
                           select q).FirstOrDefault();
            DbParameter[] paramList = new DataConvert().GetParameter(obj, query.Parameters.ToArray());
            try
            {
                IDBHelper helper = DBHelperFactory.GetHelper();
                int affectRow = helper.ExecuteCommand(query.SQL, paramList, CommandType.Text);
                if (affectRow > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="queryName">查询名称</param>
        /// <param name="obj">查询条件字典</param>
        /// <returns>更新结果</returns>
        public bool UpdateData<T>(string queryName, Dictionary<string, string> conditions)
        {
            var type = typeof(T);
            List<Query> collection = new QueryCollection().GetQuerySet(type.Name);
            Query query = (from q in collection
                           where q.Name == queryName
                           select q).FirstOrDefault();
            DbParameter[] paramList = new DataConvert().GetParameter(conditions, query.Parameters.ToArray());
            try
            {
                IDBHelper helper = DBHelperFactory.GetHelper();
                int affectRow = helper.ExecuteCommand(query.SQL, paramList, CommandType.Text);
                if (affectRow > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="T">数据对象类型</typeparam>
        /// <param name="queryName">查询名称</param>
        /// <param name="obj">数据对象</param>
        /// <returns>删除结果</returns>
        public bool DeleteData<T>(string queryName, Object obj)
        {
            var type = typeof(T);
            List<Query> collection = new QueryCollection().GetQuerySet(type.Name);
            Query query = (from q in collection
                           where q.Name == queryName
                           select q).FirstOrDefault();
            DbParameter[] paramList = new DataConvert().GetParameter(obj, query.Parameters.ToArray());
            try
            {
                IDBHelper helper = DBHelperFactory.GetHelper();
                int affectRow = helper.ExecuteCommand(query.SQL, paramList, CommandType.Text);
                if (affectRow > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="queryName">查询名称</param>
        /// <param name="obj">查询数据字典</param>
        /// <returns>删除结果</returns>
        public bool DeleteData<T>(string queryName, Dictionary<string, string> conditions)
        {
            var type = typeof(T);
            List<Query> collection = new QueryCollection().GetQuerySet(type.Name);
            Query query = (from q in collection
                           where q.Name == queryName
                           select q).FirstOrDefault();
            DbParameter[] paramList = new DataConvert().GetParameter(conditions, query.Parameters.ToArray());
            try
            {
                IDBHelper helper = DBHelperFactory.GetHelper();
                int affectRow = helper.ExecuteCommand(query.SQL, paramList, CommandType.Text);
                if (affectRow > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 获取页码数据
        /// </summary>
        /// <param name="queryName">查询名称</param>
        /// <param name="conditions">查询条件</param>
        /// <returns>返回页码</returns>
        public object GetScalar<T>(string queryName, Dictionary<string, string> conditions)
        {
            var type = typeof(T);
            List<Query> collection = new QueryCollection().GetQuerySet(type.Name);
            Query query = (from q in collection
                           where q.Name == queryName
                           select q).FirstOrDefault();
            DbParameter[] paramList = new DataConvert().GetParameter(conditions, query.Parameters.ToArray());
            try
            {
                IDBHelper helper = DBHelperFactory.GetHelper();
                Object result = helper.GetScalar(query.SQL, CommandType.Text, paramList);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        
  public object GetScalar<T>(string queryName, Dictionary<string, string> conditions,string whereString, List<Parameter> paramsList)
        {
            var type = typeof(T);
            List<Query> collection = new QueryCollection().GetQuerySet(type.Name);
            Query query = (from q in collection
                           where q.Name == queryName
                           select q).FirstOrDefault();
            if (query != null)
            {
                DbParameter[] paramList = new DataConvert().GetParameter(conditions, paramsList.ToArray());
                string sql = query.SQL;
                sql = string.Format(sql,whereString);
                try
                {
                    IDBHelper helper = DBHelperFactory.GetHelper();
                    Object result = helper.GetScalar(sql, CommandType.Text, paramList);
                    return result;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public object GetScalar<T>(string queryName)
        {
            var type = typeof(T);
            List<Query> collection = new QueryCollection().GetQuerySet(type.Name);
            Query query = (from q in collection
                           where q.Name == queryName
                           select q).FirstOrDefault();
            try
            {
                IDBHelper helper = DBHelperFactory.GetHelper();
                Object result = helper.GetScalar(query.SQL, CommandType.Text);
                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }



        /// <summary>
        /// 查询数据库获取数据集
        /// </summary>
        /// <typeparam name="T">返回对象类型</typeparam>
        /// <typeparam name="P">查询对象类型</typeparam>
        /// <param name="queryName">查询名称</param>
        /// <param name="obj">查询对象数据</param>
        /// <returns>查询结果集</returns>
        public List<T> GetDataList<T, P>(string queryName, Object obj)
        {
            var type = typeof(T);
            List<Query> collection = new QueryCollection().GetQuerySet(type.Name);
            Query query = (from q in collection
                           where q.Name == queryName
                           select q).FirstOrDefault();
            DbParameter[] paramList = new DataConvert().GetParameter(obj, query.Parameters.ToArray());
            try
            {
                IDBHelper helper = DBHelperFactory.GetHelper();
                DbDataReader reader = helper.GetDataReader(query.SQL, CommandType.Text, paramList);
                return new DataConvert().GetResultSet<T>(reader);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 查询数据库获取数据集
        /// </summary>
        /// <typeparam name="T">返回对象类型</typeparam>
        /// <param name="queryName">查询名称</param>
        /// <param name="conditions">查询条件字典</param>
        /// <returns>查询结果集</returns>
        public List<T> GetDataList<T>(string queryName, Dictionary<string, string> conditions)
        {
            var type = typeof(T);
            List<Query> collection = new QueryCollection().GetQuerySet(type.Name);
            Query query = (from q in collection
                           where q.Name == queryName
                           select q).FirstOrDefault();
            DbParameter[] paramList = new DataConvert().GetParameter(conditions, query.Parameters.ToArray());
            try
            {
                IDBHelper helper = DBHelperFactory.GetHelper();
                DbDataReader reader = helper.GetDataReader(query.SQL, CommandType.Text, paramList);
                return new DataConvert().GetResultSet<T>(reader);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 查询数据库获取数据集
        /// </summary>
        /// <typeparam name="T">返回对象类型</typeparam>
        /// <param name="queryName">查询名称</param>
        /// <returns>查询结果集</returns>
        public List<T> GetDataList<T>(string queryName)
        {
            var type = typeof(T);
            List<Query> collection = new QueryCollection().GetQuerySet(type.Name);
            Query query = (from q in collection
                           where q.Name == queryName
                           select q).FirstOrDefault();
            try
            {
                IDBHelper helper = DBHelperFactory.GetHelper();
                DbDataReader reader = helper.GetDataReader(query.SQL, CommandType.Text);
                return new DataConvert().GetResultSet<T>(reader);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">返回对象类型</typeparam>
        /// <param name="queryName">查询名称</param>
        /// <param name="conditions">查询条件</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="pageSize">每页多少条</param>
        /// <returns>结果集</returns>
        public List<T> GetDataListPager<T>(string queryName, Dictionary<string, string> conditions, int startIndex, int pageSize)
        {
            var type = typeof(T);
            List<Query> collection = new QueryCollection().GetQuerySet(type.Name);
            Query query = (from q in collection
                           where q.Name == queryName
                           select q).FirstOrDefault();
            DbParameter[] paramList = new DataConvert().GetParameter(conditions, query.Parameters.ToArray());
            string sql = query.SQL;
            sql = string.Format(sql, pageSize, startIndex);
            try
            {
                IDBHelper helper = DBHelperFactory.GetHelper();
                DbDataReader reader = helper.GetDataReader(sql, CommandType.Text, paramList);
                return new DataConvert().GetResultSet<T>(reader);
            }
            catch (Exception)
            {
                return null;
            }
        }


        public List<T> GetDataListPager<T>(string queryName, Dictionary<string, string> conditions, int startIndex, int endIndex, string whereString, List<Parameter> paramsList)
        {
            var type = typeof(T);
            List<Query> collection = new QueryCollection().GetQuerySet(type.Name);
            Query query = (from q in collection
                           where q.Name == queryName
                           select q).FirstOrDefault();
            if (query != null)
            {
                DbParameter[] paramList = new DataConvert().GetParameter(conditions, paramsList.ToArray());
                string sql = query.SQL;
                sql = string.Format(sql, startIndex, endIndex, whereString);
                try
                {
                    IDBHelper helper = DBHelperFactory.GetHelper();
                    DbDataReader reader = helper.GetDataReader(sql, CommandType.Text, paramList);
                    return new DataConvert().GetResultSet<T>(reader);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }


        public List<T> GetDataListPager<T>(string queryName, int startIndex, int pageSize)
        {
            var type = typeof(T);
            List<Query> collection = new QueryCollection().GetQuerySet(type.Name);
            Query query = (from q in collection
                           where q.Name == queryName
                           select q).FirstOrDefault();
            string sql = query.SQL;
            sql = string.Format(sql, pageSize, startIndex);
            try
            {
                IDBHelper helper = DBHelperFactory.GetHelper();
                DbDataReader reader = helper.GetDataReader(sql, CommandType.Text);
                return new DataConvert().GetResultSet<T>(reader);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 查询数据库获取数据
        /// </summary>
        /// <typeparam name="T">返回对象类型</typeparam>
        /// <typeparam name="P">查询对象类型</typeparam>
        /// <param name="queryName">查询名称</param>
        /// <param name="obj">查询对象数据</param>
        /// <returns>查询结果</returns>
        public T GetData<T, P>(string queryName, Object obj)
        {
            var type = typeof(T);
            List<Query> collection = new QueryCollection().GetQuerySet(type.Name);
            Query query = (from q in collection
                           where q.Name == queryName
                           select q).FirstOrDefault();
            DbParameter[] paramList = new DataConvert().GetParameter(obj, query.Parameters.ToArray());
            try
            {
                IDBHelper helper = DBHelperFactory.GetHelper();
                DbDataReader reader = helper.GetDataReader(query.SQL, CommandType.Text, paramList);
                return new DataConvert().GetResult<T>(reader);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// 查询数据库获取数据
        /// </summary>
        /// <typeparam name="T">返回对象类型</typeparam>
        /// <param name="queryName">查询名称</param>
        /// <param name="conditions">查询条件字典</param>
        /// <returns>查询结果</returns>
        public T GetData<T>(string queryName, Dictionary<string, string> conditions)
        {
            var type = typeof(T);
            List<Query> collection = new QueryCollection().GetQuerySet(type.Name);
            Query query = (from q in collection
                           where q.Name == queryName
                           select q).FirstOrDefault();
            DbParameter[] paramList = new DataConvert().GetParameter(conditions, query.Parameters.ToArray());
            try
            {
                IDBHelper helper = DBHelperFactory.GetHelper();
                DbDataReader reader = helper.GetDataReader(query.SQL, CommandType.Text, paramList);
                return new DataConvert().GetResult<T>(reader);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
    }
}
