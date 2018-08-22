using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CacheDemo.MemoryCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IMemoryCache _cache;

        public ValuesController(IMemoryCache cache)
        {
            _cache = cache;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //DateTime cacheEntry;

            //if (!_cache.TryGetValue("cache1", out cacheEntry))
            //{
            //    // Key not in cache, so get data.
            //    cacheEntry = DateTime.Now;

            //    var cacheEntryOptions = new MemoryCacheEntryOptions()
            //        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3));

            //    _cache.Set("cache1", cacheEntry, cacheEntryOptions);
            //}

            DateTime cacheEntry1 = DateTime.Now;

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3));

            _cache.Set("cache1", cacheEntry1, cacheEntryOptions);

            _cache.Remove("cache1");

            var cacheEntry = this._cache.Get<DateTime?>("cache1");

            

            return new string[] { DateTime.Now.ToString(), cacheEntry?.ToString() };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
