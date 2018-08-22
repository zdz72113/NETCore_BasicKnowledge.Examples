using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ORMDemo.Dapper.Infrastructure.Data;
using ORMDemo.Dapper.Model;

namespace ORMDemo.Dapper.Repository
{
    public class UowCategoryRepository : ICategoryRepository
    {
        private readonly DapperDBContext _context;
        public UowCategoryRepository(DapperDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.QueryAsync<Category>(@"SELECT Id
                                            ,Name
                                        FROM category");
        }

        public async Task<Category> GetByIDAsync(int id)
        {
            string sql = @"SELECT Id
                                ,Name
                            FROM category
                            WHERE Id = @Id";
            return await _context.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id });
        }

        public async Task<bool> AddAsync(Category category)
        {
            string sql = @"INSERT INTO category (Name) VALUES (@Name)";
            return await _context.ExecuteAsync(sql, category) > 0;
        }

        public async Task<bool> UpdateAsync(Category prod)
        {
            string sql = @"UPDATE category SET 
                                    Name = @Name
                               WHERE Id = @Id";
            return await _context.ExecuteAsync(sql, prod) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            string sql = @"DELETE FROM category
                            WHERE Id = @Id";
            return await _context.ExecuteAsync(sql, new { Id = id }) > 0;
        }
    }
}
