using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ORMDemo.EF;
using ORMDemo.EF.Model;

namespace ORMDemo.EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly BloggingContext _context;

        public BlogsController(BloggingContext context)
        {
            _context = context;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<IActionResult> GetBlogs()
        {
            var blogs = await _context.Blogs.ToListAsync();
            //var blogs = await _context.Blogs.Include(r => r.Posts).ToListAsync();
            //return _context.Blogs.FromSql($"select * from Blogs").ToList();

            //var testIQueryable = _context.Blogs.Where(r => r.Rating > 10);
            //var testIEnumerable = _context.Blogs.AsEnumerable().Where(r => r.Rating > 10);

            //var testIQueryableList = testIQueryable.ToList();
            //var testIEnumerableList = testIEnumerable.ToList();

            return Ok(blogs);
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blog = await _context.Blogs.FindAsync(id);
            //var blog = _context.Blogs.FromSql($"select * from Blogs where BlogId = {id}").Include(r => r.Posts).FirstOrDefault();

            //_context.Entry(blog)
            //    .Collection(b => b.Posts)
            //    .Load();

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        // PUT: api/Blogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog([FromRoute] int id, [FromBody] Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbModel = await _context.Blogs.FindAsync(id);

            dbModel.Url = blog.Url;
            dbModel.Rating = blog.Rating;

            //if (id != blog.BlogId)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
                //foreach (var entry in ex.Entries)
                //{
                //    if (entry.Entity is Blog)
                //    {
                //        var proposedValues = entry.CurrentValues;
                //        var databaseValues = entry.GetDatabaseValues();

                //        foreach (var property in proposedValues.Properties)
                //        {
                //            var proposedValue = proposedValues[property];
                //            var databaseValue = databaseValues[property];

                //            // TODO: decide which value should be written to database
                //            // proposedValues[property] = <value to be saved>;
                //        }

                //        // Refresh original values to bypass next concurrency check
                //        entry.OriginalValues.SetValues(databaseValues);
                //    }
                //    else
                //    {
                //        throw new NotSupportedException(
                //            "Don't know how to handle concurrency conflicts for "
                //            + entry.Metadata.Name);
                //    }
                //}
            }

            return NoContent();
        }

        // POST: api/Blogs
        [HttpPost]
        public async Task<IActionResult> PostBlog([FromBody] Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            //int affectRows = _context.Database.ExecuteSqlCommand($"Insert into Blogs([Url],[Rating])Values({blog.Url}, {blog.Rating})");

            return CreatedAtAction("GetBlog", new { id = blog.BlogId }, blog);
        }

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return Ok(blog);
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.BlogId == id);
        }
    }
}