using MySql.Data.MySqlClient;

namespace VkThread;
class DBUtils
{
    public static MySqlConnection GetDBConnection()
    {
        string host = "62.113.102.53";
        int port = 3306;
        string database = "default-db";
        string username = "default-db";
        string password = "596F756e6712@";

        return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
    }

}