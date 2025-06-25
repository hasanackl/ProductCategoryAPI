using System;
using System.Collections.Generic;

namespace ProductCategoryAPI.Models.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
    }
}
