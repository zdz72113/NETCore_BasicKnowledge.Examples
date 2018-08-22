using ORMDemo.Dapper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORMDemo.Dapper.Repository
{
    public interface IProductRepository
    {
        Task<bool> AddAsync(Product prod);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIDAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(Product prod);
    }
}
