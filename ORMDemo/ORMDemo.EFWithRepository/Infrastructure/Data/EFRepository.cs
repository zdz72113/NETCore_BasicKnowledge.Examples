using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ORMDemo.EFWithRepository.Infrastructure
{
    public class EFRepository<TDbContext, TEntity, TKey> : IRepository<TDbContext, TEntity, TKey> where TEntity : BaseEntity<TKey> where TDbContext : DbContext
    {
        protected readonly TDbContext _context;
        protected readonly DbSet<TEntity> dbSet;

        public EFRepository(TDbContext context)
        {
            this._context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetByKeyAsync(TKey id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<IList<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<Expression<Func<TEntity, object>>> includes = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await dbSet.AddAsync(entity);
            return result.Entity;
        }

        public virtual TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            this._context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual void Delete(TKey id)
        {
            TEntity entity = dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            dbSet.Remove(entity);
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            if (this._context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
        }
    }
}
