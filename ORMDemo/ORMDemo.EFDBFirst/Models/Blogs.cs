using System;
using System.Collections.Generic;

namespace ORMDemo.EFDBFirst.Models
{
    public partial class Blogs
    {
        public Blogs()
        {
            Posts = new HashSet<Posts>();
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }

        public virtual ICollection<Posts> Posts { get; set; }
    }
}
