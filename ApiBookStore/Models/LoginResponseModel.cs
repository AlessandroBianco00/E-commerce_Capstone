namespace ApiBookStore.Models
{
    public class LoginResponseModel
    {
        public int UserId { get; set; }
        public required string Email { get; set; }
        public required string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
