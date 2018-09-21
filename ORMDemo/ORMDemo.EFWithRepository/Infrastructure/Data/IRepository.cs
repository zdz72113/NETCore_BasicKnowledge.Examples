using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ORMDemo.EFWithRepository.Infrastructure
{
    public interface IRepository<TDbContext, TEntity, TKey> where TEntity : BaseEntity<TKey> where TDbContext : DbContext
    {
        Task<TEntity> GetByKeyAsync(TKey id);

        Task<IList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null);

        Task<TEntity> AddAsync(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(TKey id);

        void Delete(TEntity entity);
    }
}
