using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using NStore.Core.Services.Products.Dto;
using NStore.Web;
using Xunit;

namespace NStore.Tests.Integration.Controllers
{
    public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ProductsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory.WithWebHostBuilder(b => b.UseEnvironment("test"));
        }

        [Fact]
        public async Task given_valid_data_product_should_be_created()
        {
            var httpClient = _factory.CreateClient();

            var productDto = new ProductDetailsDto
            {
                Id = Guid.NewGuid(),
                Name = "iphone",
                Category = "phones",
                Price = 4000,
                Description = "test"
            };

            var response = await httpClient.PostAsJsonAsync("/api/products", productDto);

            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var productUrl = response.Headers.Location?.ToString();
            productUrl.Should().NotBeEmpty();
        }
    }
}