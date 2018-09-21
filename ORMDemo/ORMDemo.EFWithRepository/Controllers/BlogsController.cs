using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ORMDemo.EFWithRepository;
using ORMDemo.EFWithRepository.Infrastructure;
using ORMDemo.EFWithRepository.Model;

namespace ORMDemo.EFWithRepository.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogggingRepositoryBase<Blog, int> _blogRepository;
        private readonly IBlogggingRepositoryBase<Post, int> _postRepository;
        private readonly BloggingUnitOfWork _unitOfWork;

        public BlogsController(IBlogggingRepositoryBase<Blog, int> blogRepository, IBlogggingRepositoryBase<Post, int> postRepository, BloggingUnitOfWork unitOfWork)
        {
            _blogRepository = blogRepository;
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<IActionResult> GetBlogs()
        {
            var blogs = await _blogRepository.GetAsync();
            return Ok(blogs);
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog([FromRoute] int id)
        {
            var blog = await _blogRepository.GetByKeyAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        // POST: api/Blogs
        [HttpPost]
        public async Task<IActionResult> PostBlog([FromBody] Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _blogRepository.AddAsync(blog);
            //await _blogRepository.AddAsync(new Blog { Url = "http://sample.com/4", Rating = 0 });
            //await _postRepository.AddAsync(new Post { Title = "Title4", Content = "BlogId_1 Post_3", BlogId = 1 });
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction("GetBlog", new { id = blog.Id }, blog);
        }

        // PUT: api/Blogs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog([FromRoute] int id, [FromBody] Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dbModel = await _blogRepository.GetByKeyAsync(id);
            dbModel.Url = blog.Url;
            dbModel.Rating = blog.Rating;
            _blogRepository.Update(dbModel);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog([FromRoute] int id)
        {
            _blogRepository.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }
    }
}