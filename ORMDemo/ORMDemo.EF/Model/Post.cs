using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.EF.Model
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        public virtual PostExtension Extension { get; set; }
    }

    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public PostConfiguration()
        {
            
        }

        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(t => t.PostId);

            builder.Property(t => t.Title).HasMaxLength(100).IsRequired();

            builder.Property(t => t.Content).HasMaxLength(500);

            builder.HasOne<Blog>(p => p.Blog)
                .WithMany(b => b.Posts)
                .HasForeignKey(p => p.BlogId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
