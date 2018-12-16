using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NStore.Core.Services.Products;
using NStore.Core.Services.Products.Dto;
using NStore.Web.ViewModels;

namespace NStore.Web.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;

        public ProductDetailsDto Product { get; private set; }

        public DetailsModel(IProductService productService)
        {
            _productService = productService;
        }
        
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Product = await _productService.GetAsync(id);
            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
