using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.Dapper.Infrastructure.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}
