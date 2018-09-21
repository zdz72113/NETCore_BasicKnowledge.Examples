using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.EF.Model
{
    public class BlogTag
    {
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }

    public class BlogTagConfiguration : IEntityTypeConfiguration<BlogTag>
    {
        public void Configure(EntityTypeBuilder<BlogTag> builder)
        {
            builder.HasKey(bt => new { bt.BlogId, bt.TagId });

            builder.HasOne<Blog>(bt => bt.Blog)
                .WithMany(b => b.BlogTags)
                .HasForeignKey(bt => bt.BlogId);

            builder.HasOne<Tag>(bt => bt.Tag)
                .WithMany(t => t.BlogTags)
                .HasForeignKey(bt => bt.TagId);
        }
    }
}
