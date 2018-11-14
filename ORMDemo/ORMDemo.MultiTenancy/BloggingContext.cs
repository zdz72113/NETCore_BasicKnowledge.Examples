using Microsoft.EntityFrameworkCore;
using ORMDemo.MultiTenancy.Models;
using ORMDemo.MultiTenancy.TenantProviders;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace ORMDemo.MultiTenancy
{
    public class BloggingContext : DbContext
    {
        private Guid _tenantId;

        public BloggingContext(DbContextOptions<BloggingContext> options, ITenantProvider tenantProvider)
            : base(options)
        {
            _tenantId = tenantProvider.GetTenantId();
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BlogConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                ConfigureGlobalFiltersMethodInfo
                    .MakeGenericMethod(entityType.ClrType)
                    .Invoke(this, new object[] { modelBuilder });
            }

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            ChangeTracker.DetectChanges();

            var entities = ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity.GetType().BaseType == typeof(BaseEntity));
            foreach (var item in entities)
            {
                (item.Entity as BaseEntity).TenantId = _tenantId;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        #region

        private static MethodInfo ConfigureGlobalFiltersMethodInfo = typeof(BloggingContext).GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic);

        protected void ConfigureGlobalFilters<T>(ModelBuilder builder) where T : BaseEntity
        {
            builder.Entity<T>().HasQueryFilter(e => e.TenantId == _tenantId);
        }

        #endregion
    }
}
