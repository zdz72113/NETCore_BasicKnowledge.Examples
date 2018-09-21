using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ORMDemo.EFWithRepository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.EFWithRepository.Model
{
    public class Post : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }

    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(t => t.Title).HasMaxLength(100).IsRequired();
            builder.Property(t => t.Content).HasMaxLength(500);

            builder.HasOne<Blog>(p => p.Blog)
                .WithMany(b => b.Posts)
                .HasForeignKey(p => p.BlogId)
                .OnDelete(DeleteBehavior.Cascade);

            //Data Seeding
            builder.HasData(new Post { Id = 1, Title = "Title1", Content = "BlogId_1 Post_1", BlogId = 1 });
            builder.HasData(new Post { Id = 2, Title = "Title2", Content = "BlogId_1 Post_2", BlogId = 1 });
            builder.HasData(new Post { Id = 3, Title = "Title3", Content = "BlogId_2 Post_1", BlogId = 2 });
        }
    }
}
