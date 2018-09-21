using ORMDemo.EFWithRepository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.EFWithRepository
{
    public class BloggingUnitOfWork : UnitOfWork<BloggingContext>
    {
        public BloggingUnitOfWork(BloggingContext dbContext) : base(dbContext)
        {
        }
    }
}
