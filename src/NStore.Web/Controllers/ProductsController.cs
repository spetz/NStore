using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NStore.Core.Services.Products;
using NStore.Core.Services.Products.Dto;

namespace NStore.Web.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get([FromQuery] BrowseProducts query)
        {
            var products = await _productService.BrowseAsync(query.Name);

            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ProductDetailsDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ProductDetailsDto>> Get(Guid id)
            => Result(await _productService.GetAsync(id));

        [HttpPost]
        public async Task<ActionResult> Post(ProductDetailsDto product)
        {
            product.Id = Guid.NewGuid();
            await _productService.AddAsync(product);

            return CreatedAtAction(nameof(Get), new {id = product.Id}, null);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _productService.DeleteAsync(id);
            
            return NoContent();
        }
    }

    public class BrowseProducts
    {
        public string Name { get; set; }
    }
}