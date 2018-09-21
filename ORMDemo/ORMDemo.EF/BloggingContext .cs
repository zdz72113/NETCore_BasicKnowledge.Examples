using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ORMDemo.EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.EF
{
    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        {
            //Database.Migrate();
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostExtension> PostExtensions { get; set; }

        //private static ILoggerFactory Mlogger => new LoggerFactory()
        //    .AddDebug((categoryName, logLevel) => (logLevel == LogLevel.Information) && (categoryName == DbLoggerCategory.Database.Command.Name))
        //    .AddConsole((categoryName, logLevel) => (logLevel == LogLevel.Information) && (categoryName == DbLoggerCategory.Database.Command.Name));

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLoggerFactory(Mlogger);
            //optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BlogConfiguration());
            modelBuilder.ApplyConfiguration(new TagConfiguration());
            modelBuilder.ApplyConfiguration(new BlogTagConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.ApplyConfiguration(new PostExtensionConfiguration());
        }
    }
}
