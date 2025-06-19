using System;
using System.Collections.Generic;

namespace ProductCategoryAPI.Models
{

    public class ProductCategory
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}