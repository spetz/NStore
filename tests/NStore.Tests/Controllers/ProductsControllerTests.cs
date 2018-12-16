using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NStore.Core.Services.Products;
using NStore.Core.Services.Products.Dto;
using NStore.Web.Controllers;
using Xunit;

namespace NStore.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Fixture _fixture;
        
        public ProductsControllerTests()
        {
            _fixture = new Fixture();
        }
        
        [Fact]
        public async Task given_valid_query_get_should_return_products()
        {
            // Arrange
            var products = _fixture.CreateMany<ProductDto>();
            var query = new BrowseProducts();
            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.BrowseAsync(query.Name))
                .ReturnsAsync(products);
            var controller = new ProductsController(productServiceMock.Object);
            
            // Act
            var response = await controller.Get(query);

            // Assert
            var result = response.Result as OkObjectResult;
            result.Should().NotBeNull();
            var productsResult = result.Value as IEnumerable<ProductDto>;
            productsResult.Should().NotBeEmpty();
        }
    }
}