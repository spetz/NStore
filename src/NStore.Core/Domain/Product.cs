using System;

namespace NStore.Core.Domain
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Category { get; private set; }
        public decimal Price { get; private set; }
        public string Description { get; private set; }

        private Product()
        {
        }
        
        public Product(Guid id, string name, string category,
            decimal price, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Invalid product name.",
                    nameof(name));
            }
            
            Id = id;
            Name = name;
            Category = category;
            Price = price;
            SetDescription(description);
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Empty product description.",
                    nameof(description));
            }

            if (description.Length > 1000)
            {
                throw new ArgumentException("Too long product description.",
                    nameof(description));
            }

            Description = description;
        }
    }
}