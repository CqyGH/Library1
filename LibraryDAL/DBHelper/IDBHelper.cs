using System.Data;
using System.Data.Common;

namespace LibraryDAL.DBHelper
{
    internal interface IDBHelper
    {
        int ExecuteCommand(string sql, DbParameter[] prams, CommandType type);
        int ExecuteCommand(string sql, CommandType type);
        DbDataReader GetDataReader(string sql, CommandType type, DbParameter[] prams);
        DbDataReader GetDataReader(string sql, CommandType type);
        object GetScalar(string sql, CommandType type, DbParameter[] prams);
        object GetScalar(string sql, CommandType type);
    }
}
