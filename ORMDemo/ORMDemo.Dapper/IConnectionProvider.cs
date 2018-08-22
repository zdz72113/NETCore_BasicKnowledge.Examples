using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ORMDemo.Dapper.Infrastructure.Data
{
    public interface IConnectionProvider
    {
        IDbConnection GetDbConnection();
    }
}
