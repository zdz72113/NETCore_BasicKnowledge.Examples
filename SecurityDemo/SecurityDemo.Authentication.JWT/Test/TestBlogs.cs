using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityDemo.Authentication.JWT
{
    public static class TestBlogs
    {
        public static List<Blog> Blogs = new List<Blog>
        {
            new Blog{ Id = Guid.Parse("CA4A3FC9-42CA-47F4-B651-36A863023E75"), Name = "Paul_Blog_1", BlogUrl = "blogs/paul/1", Creator = "Paul" },
            new Blog{ Id = Guid.Parse("9C03EDA8-FBCD-4C33-B5C8-E4DFC40258D7"), Name = "Paul_Blog_2", BlogUrl = "blogs/paul/2", Creator = "Paul" },
            new Blog{ Id = Guid.Parse("E05E3625-1885-49A5-87D0-54F7EAF90C88"), Name = "Young_Blog_1", BlogUrl = "blogs/young/1", Creator = "Young" },
            new Blog{ Id = Guid.Parse("E97D5DF4-AE50-4258-84F8-0B3052EB2CB8"), Name = "Roy_Blog_1", BlogUrl = "blogs/roy/1", Creator = "Roy" },
        };
    }

    public class Blog : IResourceWithCreator
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string BlogUrl { get; set; }

        public string Creator { get; set; }
    }

    public interface IResourceWithCreator
    {
        string Creator { get; set; }
    }
}
