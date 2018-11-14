using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.MultiTenancy.Models
{
    public class Blog : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public virtual IList<Post> Posts { get; set; }
    }

    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).HasMaxLength(100).IsRequired();
            builder.Property(t => t.Url).HasMaxLength(100).IsRequired();

            builder.HasData(
                new Blog { Id = 1, Name = "Blog1 by A", Url = "http://sample.com/1", TenantId= Guid.Parse("B992D195-56CE-49BF-BFDD-4145BA9A0C13") },
                new Blog { Id = 2, Name = "Blog2 by A", Url = "http://sample.com/2", TenantId = Guid.Parse("B992D195-56CE-49BF-BFDD-4145BA9A0C13") },
                new Blog { Id = 3, Name = "Blog1 by B", Url = "http://sample.com/3", TenantId = Guid.Parse("F55AE0C8-4573-4A0A-9EF9-32F66A828D0E") });
        }
    }
}
