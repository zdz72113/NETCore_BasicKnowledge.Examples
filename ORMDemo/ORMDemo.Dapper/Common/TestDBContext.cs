using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using ORMDemo.Dapper.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.Dapper.Common
{
    public class TestDBContext : DapperDBContext
    {
        public TestDBContext(IOptions<DapperDBContextOptions> optionsAccessor) : base(optionsAccessor)
        {
        }

        protected override IDbConnection CreateConnection(string connectionString)
        {
            IDbConnection conn = new MySqlConnection(connectionString);
            return conn;
        }
    }
}
