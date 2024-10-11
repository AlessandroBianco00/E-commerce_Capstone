using ApiBookStore.DTO;

namespace ApiBookStore.Models
{
    public class LoginResponseModel
    {
        public UserLoginDto User { get; set; }
        public required string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
