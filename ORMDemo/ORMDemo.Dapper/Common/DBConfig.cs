using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;

namespace ORMDemo.Dapper
{
    public class DBConfig
    {
        //ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
        private static string DefaultSqlConnectionString = @"server=127.0.0.1;database=ormdemo;uid=root;pwd=Open0001;SslMode=none;";

        public static IDbConnection GetSqlConnection(string sqlConnectionString = null)
        {
            if (string.IsNullOrWhiteSpace(sqlConnectionString))
            {
                sqlConnectionString = DefaultSqlConnectionString;
            }
            IDbConnection conn = new MySqlConnection(sqlConnectionString);
            conn.Open();
            return conn;
        }
    }
}
