using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NStore.Web.ViewModels;

namespace NStore.Web.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ProductsProvider _productsProvider;
        
        public IEnumerable<ProductViewModel> Products { get; private set; }

        public IEnumerable<SelectListItem> Categories => new List<SelectListItem>
        {
            new SelectListItem("Phones", "Phones"),
            new SelectListItem("Computers", "Computers")
        };

        [BindProperty]
        public ProductViewModel Product { get; set; } = new ProductViewModel();

        public IndexModel(ProductsProvider productsProvider)
        {
            _productsProvider = productsProvider;
        }

        public void OnGet()
        {
            Products = _productsProvider.Products;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Product.Id = Guid.NewGuid();
            _productsProvider.Products.Add(Product);

            return RedirectToPage();
        }
    }
}
