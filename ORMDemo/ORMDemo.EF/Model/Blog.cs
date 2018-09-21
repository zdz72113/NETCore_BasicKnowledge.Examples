using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.EF.Model
{
    public class Blog
    {
        [Key]
        [Column("BlogId")]
        public int BlogId { get; set; }
        [Required]
        [MaxLength(500)]
        public string Url { get; set; }

        //[ConcurrencyCheck]
        public int Rating { get; set; }

        [Timestamp]
        public byte[] Timestamp { get; set; }

        public virtual IList<Post> Posts { get; set; }

        public virtual IList<BlogTag> BlogTags { get; set; }
    }

    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(t => t.BlogId);

            builder.Property(t => t.Url).HasColumnName("Url").HasMaxLength(500).IsRequired();

            //Data Seeding
            builder.HasData(new Blog { BlogId = 1, Url = "http://sample.com/1", Rating = 0 });
        }
    }
}
