using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NStore.Core.Services.Products.Dto;

namespace NStore.Core.Services.Products
{
    public interface IProductService
    {
        Task<ProductDetailsDto> GetAsync(Guid id);
        Task<IEnumerable<ProductDto>> BrowseAsync(string name);
        Task AddAsync(ProductDetailsDto productDto);
        Task UpdateAsync(ProductDetailsDto productDto);
        Task RemoveAsync(Guid id);
    }
}