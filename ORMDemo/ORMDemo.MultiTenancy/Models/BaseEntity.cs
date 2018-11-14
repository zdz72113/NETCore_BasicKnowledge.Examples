using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.MultiTenancy.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public Guid TenantId { get; set; }
    }
}
