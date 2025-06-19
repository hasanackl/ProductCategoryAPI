using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProductCategoryAPI.Models
{

    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }

    }
}
