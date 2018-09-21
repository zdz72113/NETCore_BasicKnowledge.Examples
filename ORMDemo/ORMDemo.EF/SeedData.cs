using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ORMDemo.EF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.EF
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BloggingContext(
                serviceProvider.GetRequiredService<DbContextOptions<BloggingContext>>()))
            {
                if (context.Blogs.Any())
                    return; // DB has been seeded

                var blogs = new List<Blog>
                {
                    new Blog
                    {
                        Url = "http://sample.com/2",
                        Rating = 0
                    },
                    new Blog
                    {
                        Url = "http://sample.com/3",
                        Rating = 0
                    },
                    new Blog
                    {
                        Url = "http://sample.com/4",
                        Rating = 0
                    }
                };

                context.Blogs.AddRange(blogs);
                context.SaveChanges();
            }
        }
    }
}
