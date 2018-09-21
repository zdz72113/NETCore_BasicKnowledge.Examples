using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORMDemo.EFWithRepository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.EFWithRepository.Model
{
    public class Blog : BaseEntity<int>
    {
        public string Url { get; set; }
        public int Rating { get; set; }

        public List<Post> Posts { get; set; }
    }

    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(t => t.Url).HasColumnName("Url").HasMaxLength(500).IsRequired();

            //Data Seeding
            builder.HasData(new Blog { Id = 1, Url = "http://sample.com/1", Rating = 0 });
            builder.HasData(new Blog { Id = 2, Url = "http://sample.com/2", Rating = 100 });
            builder.HasData(new Blog { Id = 3, Url = "http://sample.com/3", Rating = 100 });
        }
    }
}
