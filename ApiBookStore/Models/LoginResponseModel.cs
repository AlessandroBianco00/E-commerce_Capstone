using ApiBookStore.Entities;

namespace ApiBookStore.Models
{
    public class LoginResponseModel
    {
        public User User { get; set; }
        public required string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
