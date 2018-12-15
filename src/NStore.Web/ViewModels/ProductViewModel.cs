using System;
using System.ComponentModel.DataAnnotations;

namespace NStore.Web.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1,1000000)]
        public decimal Price { get; set; }
    }
}