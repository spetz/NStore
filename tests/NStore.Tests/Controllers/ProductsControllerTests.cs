using System.Threading.Tasks;
using Moq;
using NStore.Core.Services.Products;
using NStore.Web.Controllers;
using Xunit;

namespace NStore.Tests.Controllers
{
    public class ProductsControllerTests
    {
        [Fact]
        public async Task given_valid_query_get_should_return_products()
        {
            // Arrange
            var productService = new Mock<IProductService>();
            var controller = new ProductsController(productService.Object);
            
            // Act
            
            
            // Assert
        }
    }
}