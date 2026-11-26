using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<bool> NameExistsAsync(string name, int? excludeId);
        Task<bool> HasProductsAsync(int id);
        Task AddAsync(Category category);
        Task DeleteAsync(Category category);
        Task SaveChangesAsync();
    }
}
