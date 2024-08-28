using ApiBookStore.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.Models
{
    public class AuthorModel
    {
        [Required]
        [StringLength(100)]
        public required string AuthorName { get; set; }

        [Required]
        public required IFormFile Image { get; set; }

        [Required]
        [StringLength(1000)]
        public required string Description { get; set; }
    }
}
