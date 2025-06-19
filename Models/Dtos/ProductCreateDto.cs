using System;
using System.Collections.Generic;

namespace ProductCategoryAPI.Models.Dtos
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public List<Guid> CategoryIds { get; set; }
    }
}