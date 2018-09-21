using ORMDemo.EFWithRepository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.EFWithRepository
{
    public class BlogggingRepositoryBase<TEntity, TKey> : EFRepository<BloggingContext, TEntity, TKey>, IBlogggingRepositoryBase<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public BlogggingRepositoryBase(BloggingContext dbContext) : base(dbContext)
        {
        }
    }
}
