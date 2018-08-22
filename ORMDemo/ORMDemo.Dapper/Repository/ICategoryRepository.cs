using ORMDemo.Dapper.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ORMDemo.Dapper.Repository
{
    public interface ICategoryRepository
    {
        Task<bool> AddAsync(Category prod);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIDAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(Category prod);
    }
}
