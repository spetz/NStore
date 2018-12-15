using System;
using System.Collections.Generic;

namespace NStore.Web.ViewModels
{
    public class ProductsProvider
    {
        public ISet<ProductViewModel> Products { get; } = new HashSet<ProductViewModel>
        {
            new ProductViewModel
            {
                Id = Guid.NewGuid(),
                Name = "IPhone X",
                Category = "Phones",
                Description = "IPhone X description...",
                Price = 5000
            },
            new ProductViewModel
            {
                Id = Guid.NewGuid(),
                Name = "Samsung S9",
                Category = "Phones",
                Description = "Samsung S9 description...",
                Price = 3500
            },
            new ProductViewModel
            {
                Id = Guid.NewGuid(),
                Name = "Dell XPS",
                Category = "Computers",
                Description = "Dell XPS description...",
                Price = 9000
            }
        };
    }
}