using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ORMDemo.MultiTenancy.Models;

namespace ORMDemo.MultiTenancy
{
    public class TenantsContext : DbContext
    {
        public TenantsContext(DbContextOptions<TenantsContext> options)
            : base(options)
        {
        }

        private DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TenantConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public Guid GetTenantId(string host)
        {
            var tenant = Tenants.FirstOrDefault(t => t.Host == host);
            return tenant == null ? Guid.Empty : tenant.Id;
        }
    }
}
