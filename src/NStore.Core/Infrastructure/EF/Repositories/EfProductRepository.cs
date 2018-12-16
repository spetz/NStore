using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NStore.Core.Domain;
using NStore.Core.Domain.Repositories;

namespace NStore.Core.Infrastructure.EF.Repositories
{
    public class EfProductRepository : IProductRepository
    {
        private readonly NStoreContext _context;

        public EfProductRepository(NStoreContext context)
        {
            _context = context;
        }

        public async Task<Product> GetAsync(Guid id)
            => await _context.Products.SingleOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Product>> GetAllAsync()
            => await _context.Products.ToListAsync();

        public  async Task AddAsync(Product product)
        {
            if (_context.Database.IsInMemory())
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                
                return;
            }
            
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                transaction.Commit();
            }
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            _context.Products.Remove(await GetAsync(id));
            await _context.SaveChangesAsync();
        }
    }
}