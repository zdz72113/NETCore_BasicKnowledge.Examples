using ORMDemo.Dapper.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using ORMDemo.Dapper.Infrastructure.Data;

namespace ORMDemo.Dapper.Repository
{
    public class UowProductRepository : IProductRepository
    {
        private readonly DapperDBContext _context;
        public UowProductRepository(DapperDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.QueryAsync<Product>(@"SELECT Id
                                            ,Name
                                            ,Quantity
                                            ,Price
                                            ,CategoryId
                                        FROM Product");
        }

        public async Task<Product> GetByIDAsync(int id)
        {
            string sql = @"SELECT Id
                                ,Name
                                ,Quantity
                                ,Price 
                                ,CategoryId
                            FROM Product
                            WHERE Id = @Id";
            return await _context.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
        }

        public async Task<bool> AddAsync(Product prod)
        {
            string sql = @"INSERT INTO Product 
                            (Name
                            ,Quantity
                            ,Price
                            ,CategoryId)
                        VALUES
                            (@Name
                            ,@Quantity
                            ,@Price
                            ,@CategoryId)";
            return await _context.ExecuteAsync(sql, prod) > 0;
        }

        public async Task<bool> UpdateAsync(Product prod)
        {
            string sql = @"UPDATE Product SET 
                                Name = @Name
                                ,Quantity = @Quantity
                                ,Price= @Price
                                ,CategoryId= @CategoryId
                            WHERE Id = @Id";
            return await _context.ExecuteAsync(sql, prod) > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            string sql = @"DELETE FROM Product
                            WHERE Id = @Id";
            return await _context.ExecuteAsync(sql, new { Id = id }) > 0;
        }
    }
}
