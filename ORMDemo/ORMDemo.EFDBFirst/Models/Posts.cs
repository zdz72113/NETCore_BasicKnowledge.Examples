using System;
using System.Collections.Generic;

namespace ORMDemo.EFDBFirst.Models
{
    public partial class Posts
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }

        public virtual Blogs Blog { get; set; }
    }
}
