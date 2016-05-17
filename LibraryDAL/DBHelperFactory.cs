using System.Configuration;
using LibraryDAL.DBHelper;

namespace LibraryDAL
{
    internal class DBHelperFactory
    {
        public static IDBHelper GetHelper()
        {
            IDBHelper helper = null;
            string DbType = ConfigurationManager.ConnectionStrings["DBString"].ProviderName;
            switch (DbType)
            {
                //case "MySql.Data.MySqlClient": helper = new MySqlHelper(); break;
                case "System.Data.SqlClient": helper = new SqlServerHelper(); break;
            }
            return helper;
        }
    }
}
