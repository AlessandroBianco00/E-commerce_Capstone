using ApiBookStore.DTO;

namespace ApiBookStore.Interfaces
{
    public interface IOrderService
    {
        public Task<OrderDto?> GetMyOrderById(int orderId, string userId);
        public Task<List<OrderDto>> GetMyOrders(string userId);
    }
}
