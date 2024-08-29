using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.Models
{
    public class BookModel
    {
        [Required]
        [StringLength(150)]
        public required string Title { get; set; }

        [Required]
        [StringLength(2000)]
        public required string Description { get; set; }

        [Required]
        public required IFormFile Image { get; set; }

        [Required]
        [Range(0, 10000)]
        public required decimal Price { get; set; }

        [Required]
        [StringLength(100)]
        public required string Editor { get; set; }

        [Required]
        [Range(1, 10000)]
        public required int Pages { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 10)]
        [RegularExpression(@"^\d{10}(\d{3})?$", ErrorMessage = "Invalid ISBN format")]
        public required string ISBN { get; set; }

        [Required]
        [StringLength(30)]
        public required string Language { get; set; }

        [Required]
        public required DateOnly PublicationDate { get; set; }

        [Required]
        [Range(0, 10000)]
        public required int QuantityAvailable { get; set; }

        public required int AuthorId { get; set; }

        public required int TranslatorId { get; set; }

        public required int DiscountId { get; set; }
    }
}
