using ApiBookStore.DTO;

namespace ApiBookStore.Interfaces
{
    public interface ICartService
    {
        public Task<CartDto?> GetMyCart(string userId);
    }
}
