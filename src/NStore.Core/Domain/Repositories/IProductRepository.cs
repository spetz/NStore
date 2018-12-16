using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NStore.Core.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}