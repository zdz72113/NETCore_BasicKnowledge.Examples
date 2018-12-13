using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityDemo.Authentication.JWT
{
    public interface IBlogService
    {
        IList<Blog> GetAllBlogs();
        Blog GetBlogById(Guid id);
        bool Add(Blog blog);
        bool Update(Blog blog);
        bool Delete(Blog blog);
    }

    public class BlogService : IBlogService
    {
        public bool Add(Blog blog)
        {
            return true;
        }

        public bool Delete(Blog blog)
        {
            return true;
        }

        public IList<Blog> GetAllBlogs()
        {
            return TestBlogs.Blogs;
        }

        public Blog GetBlogById(Guid id)
        {
            return TestBlogs.Blogs.SingleOrDefault(r => r.Id.Equals(id));
        }

        public bool Update(Blog blog)
        {
            return true;
        }
    }
}
