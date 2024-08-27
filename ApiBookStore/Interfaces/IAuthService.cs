using ApiBookStore.Models.Entities;

namespace ApiBookStore.Interfaces
{
    public interface IAuthService
    {
        Task<User> Login(string username, string password);
        public Task<User> CreateUser(User user);
        public Task<User> Register(User user);
    }
}
