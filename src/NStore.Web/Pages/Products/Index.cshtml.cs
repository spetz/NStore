using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NStore.Web.ViewModels;

namespace NStore.Web.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ProductsProvider _productsProvider;
        
        public IEnumerable<ProductViewModel> Products { get; private set; }

        public IndexModel(ProductsProvider productsProvider)
        {
            _productsProvider = productsProvider;
        }

        public void OnGet()
        {
            Products = _productsProvider.Products;
        }
    }
}
