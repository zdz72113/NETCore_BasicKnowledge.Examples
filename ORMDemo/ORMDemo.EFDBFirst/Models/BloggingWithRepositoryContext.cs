using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ORMDemo.EFDBFirst.Models
{
    public partial class BloggingWithRepositoryContext : DbContext
    {
        public BloggingWithRepositoryContext()
        {
        }

        public BloggingWithRepositoryContext(DbContextOptions<BloggingWithRepositoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Blogs> Blogs { get; set; }
        public virtual DbSet<Posts> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=CD02SZV3600503\\SQLEXPRESS;Database=BloggingWithRepository;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Blogs>(entity =>
            {
                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Posts>(entity =>
            {
                entity.HasIndex(e => e.BlogId);

                entity.Property(e => e.Content).HasMaxLength(500);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.BlogId);
            });
        }
    }
}
