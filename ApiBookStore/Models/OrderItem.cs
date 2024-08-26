using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ApiBookStore.Models;

namespace PizzeriaWebApp.Models.Entities
{
    public class OrderItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [ForeignKey("BookId")]
        public Book Book { get; set; }

    }
}
