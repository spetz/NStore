using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NStore.Core.Domain;
using NStore.Core.Domain.Repositories;
using NStore.Core.Services.Products;
using Xunit;

namespace NStore.Tests.Services
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task given_valid_id_get_should_return_product()
        {
            // Arrange
            var id = Guid.NewGuid();
            var product = new Product(id, "iphone", "phones", 5000, "description");
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(x => x.GetAsync(id)).ReturnsAsync(product);
            var productService = new ProductService(productRepositoryMock.Object);
            
            // Act
            var dto = await productService.GetAsync(id);
            
            // Assert
            dto.Should().NotBeNull();
            dto.Id.Should().Be(product.Id);
            dto.Name.Should().Be(product.Name);
            dto.Category.Should().Be(product.Category);
            dto.Description.Should().Be(product.Description);
            dto.Price.Should().Be(product.Price);
            productRepositoryMock.Verify(x => x.GetAsync(id), Times.Once);
        }
    }
}