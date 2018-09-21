using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.EFWithRepository.Infrastructure
{
    public abstract class BaseEntity<TKey>
    {
        public virtual TKey Id { get; set; }
    }
}
