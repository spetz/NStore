using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NStore.Web.ViewModels;

namespace NStore.Web.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly ProductsProvider _productsProvider;
        
        public ProductViewModel Product { get; private set; }

        public DetailsModel(ProductsProvider productsProvider)
        {
            _productsProvider = productsProvider;
        }
        
        public IActionResult OnGet(Guid id)
        {
            Product = _productsProvider.Products.SingleOrDefault(p => p.Id == id);
            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
