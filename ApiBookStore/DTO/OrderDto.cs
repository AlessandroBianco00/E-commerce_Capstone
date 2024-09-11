using ApiBookStore.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiBookStore.DTO
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public int? ShippingAddressId { get; set; }
        public DateOnly OrderDate { get; set; }
        public int Status { get; set; }
        public List<OrderItemDto> Books { get; set; } = new List<OrderItemDto>();
    }
}
