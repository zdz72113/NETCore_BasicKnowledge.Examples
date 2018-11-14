using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.MultiTenancy.Models
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
    }

    public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).HasMaxLength(100).IsRequired();
            builder.Property(t => t.Host).HasMaxLength(100).IsRequired();

            builder.HasData(
                new Tenant { Id = Guid.Parse("B992D195-56CE-49BF-BFDD-4145BA9A0C13"), Name = "Customer A", Host = "localhost:5200" },
                new Tenant { Id = Guid.Parse("F55AE0C8-4573-4A0A-9EF9-32F66A828D0E"), Name = "Customer B", Host = "localhost:5300" });
        }
    }
}
