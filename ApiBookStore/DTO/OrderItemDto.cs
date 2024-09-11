using ApiBookStore.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.DTO
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public BookSearchDto Book { get; set; }
    }
}
