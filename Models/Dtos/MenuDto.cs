using System;
using System.Collections.Generic;

namespace ProductCategoryAPI.Dtos
{
    public class MenuDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public List<MenuDto> Children { get; set; } = new List<MenuDto>();
    }
}
