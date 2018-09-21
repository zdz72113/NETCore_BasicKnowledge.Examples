using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.EFWithRepository.Infrastructure
{
    public class Page<T>
    {
        /// <summary>
        /// the current page index.
        /// </summary>
        public long CurrentPage { get; set; }

        /// <summary>
        /// the total pages count.
        /// </summary>
        public long TotalPages { get; set; }

        /// <summary>
        /// the total items count.
        /// </summary>
        public long TotalItems { get; set; }

        /// <summary>
        /// the items count per page.
        /// </summary>
        public long ItemsPerPage { get; set; }

        /// <summary>
        /// the items.
        /// </summary>
        public List<T> Items { get; set; }
    }
}
