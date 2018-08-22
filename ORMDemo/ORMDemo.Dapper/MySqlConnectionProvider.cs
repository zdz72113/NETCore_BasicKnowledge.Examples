using MySql.Data.MySqlClient;
using ORMDemo.Dapper.Infrastructure.Data;
using System.Data;

namespace ORMDemo.Dapper
{
    public class MySqlConnectionProvider : IConnectionProvider
    {
        private IDbConnection conn;

        public IDbConnection GetDbConnection()
        {
            if (conn == null)
            {
                conn = new MySqlConnection(@"server=127.0.0.1;database=ormdemo;uid=root;pwd=Open0001;");
            }
            return conn;
        }
    }
}
