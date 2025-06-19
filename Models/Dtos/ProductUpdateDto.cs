using System;
using System.Collections.Generic;

namespace ProductCategoryAPI.Models.Dtos
{
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public List<Guid> CategoryIds { get; set; }
    }
}
