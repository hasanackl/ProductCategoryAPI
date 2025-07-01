using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductCategoryAPI.Models
{
    public class Menu
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        public Guid? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public Menu Parent { get; set; }

        public ICollection<Menu> Children { get; set; } = new List<Menu>();
    }
}
