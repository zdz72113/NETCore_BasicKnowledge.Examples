using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.EFWithRepository.Infrastructure
{
    public interface IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        Task<int> SaveChangesAsync();
    }
}
