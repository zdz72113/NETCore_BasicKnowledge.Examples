using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.EF.Model
{
    public class PostExtension
    {
        public int PostId { get; set; }
        public string ExtensionField1 { get; set; }

        public virtual Post Post { get; set; }
    }

    public class PostExtensionConfiguration : IEntityTypeConfiguration<PostExtension>
    {
        public PostExtensionConfiguration()
        {

        }

        public void Configure(EntityTypeBuilder<PostExtension> builder)
        {
            builder.HasKey(t => t.PostId);

            //builder.HasOne(e => e.Post)
            //    .WithOne(p => p.Extension)
            //    .HasForeignKey<PostExtension>(e => e.PostId)
            //    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
