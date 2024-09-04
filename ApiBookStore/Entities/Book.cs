using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.Models.Entities
{
    public class Book
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Image { get; set; }

        [Required]
        [Range(0, 10000)]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(100)]
        public string Editor { get; set; }

        [Required]
        [Range(1, 10000)]
        public int Pages { get; set; }

        [Required]
        [StringLength(13, MinimumLength = 10)]
        [RegularExpression(@"^\d{10}(\d{3})?$", ErrorMessage = "Invalid ISBN format")]
        public string ISBN { get; set; }

        [Required]
        [StringLength(30)]
        public string Language { get; set; }

        [Required]
        public DateOnly PublicationDate { get; set; }

        [Required]
        [Range(0, 10000)]
        public int QuantityAvailable { get; set; }

        public int AuthorId { get; set; }

        public int TranslatorId { get; set; }

        public int DiscountId { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

        [ForeignKey("TranslatorId")]
        public Translator Translator { get; set; }

        [ForeignKey("DiscountId")]
        public Discount Discount { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>();

        public List<Review> Reviews { get; set; } = new List<Review>();

        public List<Wishlist> Wishlists { get; set; } = new List<Wishlist>();

        public List<CartItem> Carts { get; set; } = new List<CartItem>();

        public List<OrderItem> Orders { get; set; } = new List<OrderItem>();
    }
}
