using System;
using System.Collections.Generic;

namespace ProductCategoryAPI.Models.Dtos
{
    public class ProductCreateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Guid> CategoryIds { get; set; }
    }
}