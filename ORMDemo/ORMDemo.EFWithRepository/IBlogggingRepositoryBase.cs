using ORMDemo.EFWithRepository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.EFWithRepository
{
    public interface IBlogggingRepositoryBase<TEntity, TKey> : IRepository<BloggingContext, TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
    }
}
