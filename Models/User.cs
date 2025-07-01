using System;
using System.ComponentModel.DataAnnotations;

namespace ProductCategoryAPI.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Role { get; set; } = "User";

        public bool IsActive { get; set; } = true;
    }
}
