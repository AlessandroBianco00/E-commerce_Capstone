using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.Entities
{
    public class OrderItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemId { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Quantity { get; set; }
        [Required]
        [Range(0, 10000)]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
        public int OrderId { get; set; }

        public int BookId { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}
