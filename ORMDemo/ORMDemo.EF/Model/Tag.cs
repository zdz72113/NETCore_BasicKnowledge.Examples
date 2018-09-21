using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.EF.Model
{
    public class Tag
    {
        public int TagId { get; set; }
        public string TagName { get; set; }

        public virtual IList<BlogTag> BlogTags { get; set; }
    }

    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(t => t.TagName).HasMaxLength(20).IsRequired();
        }
    }
}
