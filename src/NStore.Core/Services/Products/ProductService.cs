using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NStore.Core.Domain;
using NStore.Core.Domain.Repositories;
using NStore.Core.Services.Products.Dto;

namespace NStore.Core.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task<ProductDetailsDto> GetAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);

            return product == null
                ? null
                : new ProductDetailsDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Category = product.Category,
                    Description = product.Description
                };
        }

        public async Task<IEnumerable<ProductDto>> BrowseAsync(string name)
        {
            var products = await _productRepository.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(name))
            {
                products = products.Where(p => p.Name.Contains(name));
            }

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Category = p.Category
            });
        }

        public async Task AddAsync(ProductDetailsDto productDto)
        {
            var product = new Product(productDto.Id, productDto.Name,
                productDto.Category, productDto.Price, productDto.Description);
            await _productRepository.AddAsync(product);
        }

        public async Task UpdateAsync(ProductDetailsDto productDto)
        {
            var product = await _productRepository.GetAsync(productDto.Id);
            if (product == null)
            {
                throw new ArgumentException($"Product with id: '{productDto.Id}' " +
                                            "was not found", nameof(productDto.Id));
            }

            product.SetDescription(productDto.Description);
            await _productRepository.UpdateAsync(product);
        }

        public async Task RemoveAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            if (product == null)
            {
                throw new ArgumentException($"Product with id: '{id}' " +
                                            "was not found", nameof(id));
            }
            
            await _productRepository.DeleteAsync(id);
        }
    }
}