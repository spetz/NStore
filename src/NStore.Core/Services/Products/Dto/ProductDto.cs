using System;

namespace NStore.Core.Services.Products.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}