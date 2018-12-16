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
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IEnumerable<ProductDto> Products { get; private set; }

        public IEnumerable<SelectListItem> Categories => new List<SelectListItem>
        {
            new SelectListItem("Phones", "Phones"),
            new SelectListItem("Computers", "Computers")
        };

        [BindProperty]
        public ProductDetailsDto Product { get; set; } = new ProductDetailsDto();

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public async Task OnGetAsync()
        {
            Products = await _productService.BrowseAsync(string.Empty);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Product.Id = Guid.NewGuid();
            await _productService.AddAsync(Product);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _productService.DeleteAsync(id);

            return RedirectToPage();
        }
    }
}
