using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProductCategoryAPI.Models
{

    public class ProductCategory
    {
        public Guid ProductId { get; set; }
        [JsonIgnore]
        public Product Product { get; set; }
        public Guid CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
    }
}