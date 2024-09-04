using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ApiBookStore.Models.Entities
{
    // Nullable asssegnati per risolvere errore di cancellazione a cascata
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public int? UserId { get; set; }

        public int? ShippingAddressId { get; set; }

        [Required]
        public DateOnly OrderDate { get; set; }

        [Required]
        public int Status { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [ForeignKey("ShippingAddressId")]
        public ShippingAddress? ShippingAddress { get; set; }

        public List<OrderItem> Books { get; set; } = new List<OrderItem>();
    }
}
