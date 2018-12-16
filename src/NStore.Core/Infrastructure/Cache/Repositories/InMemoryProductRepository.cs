using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NStore.Core.Domain;
using NStore.Core.Domain.Repositories;

namespace NStore.Core.Infrastructure.Cache.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly ISet<Product> _products = new HashSet<Product>();

        public async Task<Product> GetAsync(Guid id)
            => await Task.FromResult(_products.SingleOrDefault(p => p.Id == id));

        public async Task<IEnumerable<Product>> GetAllAsync()
            => await Task.FromResult(_products);

        public async Task AddAsync(Product product)
        {
            _products.Add(product);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Product product)
            => await Task.CompletedTask;

        public async Task DeleteAsync(Guid id)
        {
            _products.Remove(_products.SingleOrDefault(p => p.Id == id));
            await Task.CompletedTask;
        }
    }
}