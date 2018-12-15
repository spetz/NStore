using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NStore.Web.ViewModels;

namespace NStore.Web.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly ProductsProvider _productsProvider;

        public ProductsController(ProductsProvider productsProvider)
        {
            _productsProvider = productsProvider;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductViewModel>> Get([FromQuery] BrowseProducts query)
        {
            var products = _productsProvider.Products.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                products = products.Where(p => p.Name.Contains(query.Name));
            }

            return Ok(products);
        }

        [HttpGet("{id:guid}")]
        public ActionResult<ProductViewModel> Get(Guid id)
            => Result(_productsProvider.Products.SingleOrDefault(p => p.Id == id));

        [HttpPost]
        public ActionResult Post(ProductViewModel product)
        {
            product.Id = Guid.NewGuid();
            _productsProvider.Products.Add(product);

            return CreatedAtAction(nameof(Get), new {id = product.Id}, null);
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Delete(Guid id)
        {
            var product = _productsProvider.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            
            _productsProvider.Products.Remove(product);
            
            return NoContent();
        }
    }

    public class BrowseProducts
    {
        public string Name { get; set; }
    }
}