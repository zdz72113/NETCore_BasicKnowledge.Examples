using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.Dapper.Infrastructure.Data
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
